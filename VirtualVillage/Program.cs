using VirtualVillage.Entities;

namespace VirtualVillage;

internal class Program
{
    static void Main()
    {
        var world = new World();

        var forest = new ForestEntity(new Position(5, 1));
        var mine = new MineEntity(new Position(-3, 1));

        var storehouse = new StorehouseEntity(new Position(0, 0));

        world.Entities.AddRange([forest, mine, storehouse]);

        var lumberjack = new Villager("Leif", storehouse.Position);

        var lumberGoal = new GoapState();
        lumberGoal.Set(WorldKeys.StorehouseWood, 5);

        var miner = new Villager("Bjorn", storehouse.Position);

        var minerGoal = new GoapState();
        minerGoal.Set(WorldKeys.StorehouseOre, 3);

        world.Villagers.AddRange([lumberjack, miner]);
        foreach (var v in world.Villagers)
            v.CollectActions(world);

        var planner = new GoapPlanner(
            logger: new ConsoleGoapLogger(),
            heuristic: new PositionDistanceHeuristic());

        // Plan separately (important!)
        lumberjack.SetPlan(planner.Plan(
            lumberjack.State.Clone(),
            lumberGoal,
            lumberjack.AvailableActions)!);

        miner.SetPlan(planner.Plan(
            miner.State.Clone(),
            minerGoal,
            miner.AvailableActions)!);

        // Run simulation
        Console.WriteLine("Starting simulation");
        for (int i = 0; i < 35; i++)
            world.Step();

        Console.Write("Simulation completed");
        Console.ReadKey();
    }
}
