namespace VirtualVillage;

class Program
{

    static void Main()
    {
        var agent = new Agent("Bob", new Location(0, 0));
        var forest = new Forest(new Location(5, 5));
        var storehouse = new Storehouse(new Location(0, 1));

        var world = new World();
        world.Agents.Add(agent);
        world.Entities.Add(forest);
        world.Entities.Add(storehouse);
        Console.WriteLine(world);

        var state = new WorldState();
        agent.Update(world, state);

        var chop = forest!.GetProvidedActions().First();
        Console.WriteLine(chop.Precondition(state));

        int iteration = 0;
        while (true)
        {
            Console.WriteLine($"\n=== Simulation Tick {iteration} ===");
            iteration++;

            Console.WriteLine();
            Console.Write("Step simulation [any key] or Quit [q]");
            var keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Q)
                break;
        }
    }
}
