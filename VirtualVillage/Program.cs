namespace VirtualVillage;

/*
show how to prevent action explosion cleanly
*/

class Program
{
    static void Main()
    {
        var world = new WorldState
        {
            Agents = new Dictionary<string, AgentState>
            {
                ["villager_1"] = new AgentState("villager_1", new Location(0, 0), [], 10, 10),
                ["villager_2"] = new AgentState("villager_2", new Location(2, 0), [], 10, 10)
            },
            Entities = new Dictionary<string, EntityState>
            {
                ["forest_1"] = new EntityState("forest_1", "Forest", new Location(5, 5), new Dictionary<string, int> { { "Trees", 5 } }),
                ["forest_2"] = new EntityState("forest_2", "Forest", new Location(8, 2), new Dictionary<string, int> { { "Trees", 3 } }),
                ["storehouse_1"] = new EntityState("storehouse_1", "Storehouse", new Location(0, 0), []),
                ["home_1"] = new EntityState("home_1", "Home", new Location(1,3), [])
            }
        };

        // Create simulation
        var sim = new Simulation(world);

        // Add providers
        sim.Providers.Add(new Forest("forest_1", new Location(5, 5)));
        sim.Providers.Add(new Forest("forest_2", new Location(8, 2)));
        sim.Providers.Add(new Storehouse("storehouse_1", new Location(0, 0)));
        sim.Providers.Add(new Home("home_1", new Location(1, 3)));
        sim.Providers.Add(new MovementProvider());

        // Add agents with goals
        sim.Agents.Add(new SimAgent("villager_1", goal: s => s.Entities["storehouse_1"].Resources.GetValueOrDefault("Wood") >= 6));
        //sim.Agents.Add(new SimAgent("villager_2", goal: s => s.Agents["villager_2"].Inventory.GetValueOrDefault("Wood") >= 1));

        int iteration = 0;
        while (true)
        {
            Console.WriteLine($"\n=== Simulation Tick {iteration} ===");
            iteration++;

            sim.Tick();

            // Print summary of world state
            Console.WriteLine("State:");
            foreach (var agent in world.Agents.Values)
                Console.WriteLine($"Agent {agent.Id} @ {agent.Location} | Inventory: {string.Join(", ", agent.Inventory.Select(kv => $"{kv.Key}:{kv.Value}"))}");
            foreach (var entity in world.Entities.Values)
                Console.WriteLine($"{entity.Kind} {entity.Id} @ {entity.Location} | Resources: {string.Join(", ", entity.Resources.Select(kv => $"{kv.Key}:{kv.Value}"))}");

            Console.WriteLine();
            Console.Write("Step simulation [any key] or Quit [q]");
            var keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Q)
                break;
        }
    }
}
