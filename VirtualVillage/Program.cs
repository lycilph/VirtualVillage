namespace VirtualVillage;

class Program
{
    // TODO: Add statekeys to agent states
    // TODO: Add multiple agents (miner, lumberjack and blacksmith)
    // * Only miner can mine
    // * Only lumberjack can cut wood
    // * Only blacksmith can forge tools

    static void Main()
    {
        var agent = new Agent("Bob", new LumberjackJob(), new Location(0, 0));
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

        storehouse.Axes = 0;
        storehouse.Pickaxes = 1;



        var actions = world.GetActions();
        var agent_actions = actions.Where(agent.Job.AllowsAction).ToList();



        //var state = world.GetWorldState(world.Agents.First());
        //var actions = world.GetActions();
        //var goal = GoalFactory.StoreAxe(world, agent);
        //var tracer = new MinimalConsolePlannerTracer();
        //var plan = Planner.Plan(state, actions, goal, tracer);

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
