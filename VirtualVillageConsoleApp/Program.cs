using VirtualVillageConsoleApp.Actions;

namespace VirtualVillageConsoleApp;

internal class Program
{
    //static Simulation sim = new();

    static void Main()
    {
        List<GoapAction> actions =
            [
                new IdleAction(),
                new EatAction(),
                new SleepAction(),
                new FarmAction(),
                new GoToAction(),
                new StoreResourceAction()
            ];
        var planner = new GoapPlanner();

        var plan = planner.CreatePlan();
        foreach ( var action in plan )
            Console.WriteLine($"Action: {action}");

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();

        //SetupSimulation();

        //var sim_running = true;
        //while (sim_running)
        //{
        //    Console.Clear();
        //    Console.WriteLine("1. Advance simulation");
        //    Console.WriteLine("2. Quit");

        //    sim.Render();

        //    var key = Console.ReadKey();
        //    switch (key.KeyChar)
        //    {
        //        case '1':
        //            sim.Update();
        //            break;
        //        case '2':
        //            sim_running = false;
        //            break;
        //    }
        //}
    }

    //private static void SetupSimulation()
    //{
    //    var agent = new Agent { Name = "Bob" };
    //    agent.State.Set("hunger", 0);
    //    agent.State.Set("food", 0);
    //    agent.Goals.Add(new SatiateHungerGoal());
    //    agent.Goals.Add(new IdleGoal());
    //    sim.Agents.Add(agent);

    //    var tree = new Item { Name = "Tree", Position = new Position(10, 10) };
    //    sim.Items.Add(tree);

    //    var home = new Item { Name = "Home", Position = new Position(0, 0) };
    //    sim.Items.Add(home);
    //}
}
