namespace VirtualVillage;

internal class Program
{
    static void Main()
    {
        var forest = new Position(5, 1);
        var storehousePos = new Position(0, 0);

        var lumberjack = new Villager("Leif", storehousePos);

        lumberjack.State.Set("WoodCarried", 0);
        lumberjack.State.Set("StorehouseWood", 0);

        lumberjack.AvailableActions.AddRange(new GoapAction[]
        {
            new MoveAction(forest),
            new MoveAction(storehousePos),
            new ChopWoodAction(forest),
            new DepositWoodAction(storehousePos)
        });

        var lumberGoal = new GoapState();
        lumberGoal.Set("StorehouseWood", 5);

        var mine = new Position(-3, 1);

        var miner = new Villager("Bjorn", storehousePos);

        miner.State.Set("OreCarried", 0);
        miner.State.Set("StorehouseOre", 0);
        
        miner.AvailableActions.AddRange(new GoapAction[]
        {
            new MoveAction(mine),
            new MoveAction(storehousePos),
            new MineOreAction(mine),
            new DepositOreAction(storehousePos)
        });

        var minerGoal = new GoapState();
        minerGoal.Set("StorehouseOre", 3);

        var world = new World
        {
            Storehouse = new Storehouse(storehousePos)
        };

        world.Villagers.Add(lumberjack);
        world.Villagers.Add(miner);

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
