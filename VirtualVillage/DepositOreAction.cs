namespace VirtualVillage;

public class DepositOreAction : GoapAction
{
    private readonly Position _storehouse;

    public DepositOreAction(Position storehouse)
    {
        _storehouse = storehouse;
    }

    public override string Name => "DepositOre";

    public override bool CanRun(GoapState state)
        => state.Get<Position>("Position").Equals(_storehouse)
           && state.Get<int>("OreCarried") > 0;

    public override void Apply(GoapState state)
    {
        var carried = state.Get<int>("OreCarried");

        state.Set("OreCarried", 0);
        state.Set("StorehouseOre",
            state.Get<int>("StorehouseOre") + carried);
    }

    public override int GetCost(GoapState state) => 1;

    public override void Execute(World world, Villager villager)
    {
        world.Storehouse.AddOre(1);
    }
}
