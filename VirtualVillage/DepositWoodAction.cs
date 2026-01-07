namespace VirtualVillage;

public class DepositWoodAction : GoapAction
{
    private readonly Position _storehouse;

    public DepositWoodAction(Position storehouse)
    {
        _storehouse = storehouse;
    }

    public override string Name => "DepositWood";

    public override bool CanRun(GoapState state)
        => state.Get<Position>("Position").Equals(_storehouse)
           && state.Get<int>("WoodCarried") > 0;

    public override void Apply(GoapState state)
    {
        var carried = state.Get<int>("WoodCarried");

        state.Set("WoodCarried", 0);
        state.Set("StorehouseWood",
            state.Get<int>("StorehouseWood") + carried);
    }

    public override int GetCost(GoapState state) => 1;

    public override void Execute(World world, Villager villager)
    {
        world.Storehouse.AddWood(1);
    }
}
