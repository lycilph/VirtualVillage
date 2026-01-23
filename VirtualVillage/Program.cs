using VirtualVillage.Agents;
using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Entities;
using VirtualVillage.Jobs;
using VirtualVillage.Planning;

namespace VirtualVillage;

class Program
{
    // TODO: Make actions sub-classes...
    // * Craftaxe + forge

    // TODO: Add plan execution
    // * Implement CanExecute, Execute, IsComplete for all actions
    // * Add tracer (see planner tracer implementation)

    // TODO: Add proper inventory management
    // * for agents and the store house
    // * Generate pickup and deposit actions in the storehouse automatically

    // TODO: Name generator

    static void Main()
    {
        var lumberjack = new Agent("Bjorn Oakhand", new LumberjackJob(), new Location(0, 0));
        var miner = new Agent("Borin Ironvein", new MinerJob(), new Location(0, 0));
        var blacksmith = new Agent("Magnus Ironwright", new BlacksmithJob(), new Location(0, 0));
        var forest = new Forest(new Location(5, 5), 5);
        var mine = new Mine(new Location(-5, 2), 10);
        var storehouse = new Storehouse(new Location(0, 1));
        var forge = new Forge(new Location(-2, -1));

        var world = new World();
        world.Agents.Add(lumberjack);
        world.Agents.Add(miner);
        world.Agents.Add(blacksmith);
        world.Entities.Add(forest);
        world.Entities.Add(mine);
        world.Entities.Add(storehouse);
        world.Entities.Add(forge);
        Console.WriteLine(world);

        storehouse.Axes = 0;
        storehouse.Pickaxes = 0;

        var tracer = new MinimalConsolePlannerTracer();
        foreach (var agent in world.Agents)
        {
            Console.WriteLine(agent.Name);
            var state = world.GetWorldState(agent);
            var actions = world.GetActions().Where(agent.Job.AllowsAction).ToList();
            var goal = agent.Job.GetGoals(world, agent).First();
            var plan = Planner.Plan(state, actions, goal, tracer);
        }

        //var state = world.GetWorldState(lumberjack);
        //var actions = world.GetActions().Where(lumberjack.Job.AllowsAction).ToList();
        //var goal = lumberjack.Job.GetGoals(world, lumberjack).First();
        //var plan = Planner.Plan(state, actions, goal, tracer);

        //lumberjack.AssignPlan(plan!);

        int iteration = 0;
        while (true)
        {
            Console.WriteLine($"\n=== Simulation Tick {iteration} ===");
            iteration++;
            
            //lumberjack.Tick(world);
            //world.Render();

            Console.WriteLine();
            Console.Write("Step simulation [any key] or Quit [q]");
            var keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Q)
                break;
        }
    }
}
