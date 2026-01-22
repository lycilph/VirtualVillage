namespace VirtualVillage;

class Program
{

    static void Main()
    {
        var agent = new Agent("Bob", new Location(0, 0));
        var forest = new Forest(new Location(5, 5), 5);
        var mine = new Mine(new Location(-5, 2), 10);
        var storehouse = new Storehouse(new Location(0, 1));
        var forge = new Forge(new Location(-2, -1));

        var world = new World();
        world.Agents.Add(agent);
        world.Entities.Add(forest);
        world.Entities.Add(mine);
        world.Entities.Add(storehouse);
        world.Entities.Add(forge);
        Console.WriteLine(world);

        storehouse.StoredPickaxes = 1;

        var state = world.GetWorldState(world.Agents.First());
        Console.WriteLine(state);

        var actions = world.GetActions();

        //var goal = new Goal.Builder("Collect Wood")
        //    .WithDesiredState(s => s.Get<int>("stored_wood") > 2)
        //    .Build();
        //var goal = new Goal.Builder("Collect Ore")
        //    .WithDesiredState(s => s.Get<int>("stored_ore") > 1)
        //    .Build();
        var goal = new Goal.Builder("Craft Axe")
            .WithDesiredState(s => s.Get<int>("stored_axes") > 0)
            .Build();
        Console.WriteLine(goal);

        Console.WriteLine("Plan:");
        var plan = Planner.Plan(state, actions, goal);
        if (plan == null || plan.Count == 0)
            Console.WriteLine("No plan found...");
        else
            foreach (var action in plan)
                Console.WriteLine(action);

        Console.Write("Press any key to continue...");
        Console.ReadKey();

        //int iteration = 0;
        //while (true)
        //{
        //    Console.WriteLine($"\n=== Simulation Tick {iteration} ===");
        //    iteration++;

        //    Console.WriteLine();
        //    Console.Write("Step simulation [any key] or Quit [q]");
        //    var keyInfo = Console.ReadKey();

        //    if (keyInfo.Key == ConsoleKey.Q)
        //        break;
        //}
    }
}
