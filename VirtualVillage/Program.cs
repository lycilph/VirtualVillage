namespace VirtualVillage;

/*
add planner visualization using this state model

refactor entities to an IActionProvider

add distance-based cost heuristics using TargetEntityId

show how to prevent action explosion cleanly
*/

class Program
{
    static void Main()
    {
        var providers = new List<IActionProvider>
        {
            new Forest("forest_1", new Location(5, 5)),
            new Storehouse("storehouse_1", new Location(0, 0)),
            new MovementProvider()
        };

        var world = new WorldState
        {
            Agents =
            {
                ["villager_1"] = new AgentState(
                    "villager_1",
                    new Location(0, 0),
                    new Dictionary<string, int>())
            },
            Entities =
            {
                ["forest_1"] = new EntityState(
                    "forest_1", "Forest",
                    new Location(5, 5),
                    new Dictionary<string, int> { ["Trees"] = 3 }),

                ["storehouse_1"] = new EntityState(
                    "storehouse_1", "Storehouse",
                    new Location(0, 0),
                    new Dictionary<string, int>())
            }
        };

        var actions = providers
                .SelectMany(p => p.GetActions("villager_1", world))
                .ToList();

        Predicate<WorldState> goal = s => s.Entities["storehouse_1"].Resources.GetValueOrDefault("Wood") > 1;

        Console.WriteLine(world);
        foreach (var a in actions)
            Console.WriteLine("Action: " + a.Name);

        var plan = Planner.Plan(world, "villager_1", goal, actions, true);
        foreach (var a in plan)
            Console.WriteLine("Action: " + a.Name);
        Console.WriteLine(world);

        int iteration = 0;
        while (true)
        {
            Console.WriteLine($"=== Village Status (iteration {iteration})===");
            iteration++;

            Console.WriteLine();
            Console.Write("Step simulation [any key] or Quit [q]");
            var keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Q)
                break;
        }
    }
}
