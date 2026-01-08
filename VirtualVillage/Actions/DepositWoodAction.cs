using VirtualVillage.Entities;

namespace VirtualVillage.Actions;

public class DepositWoodAction : GoapAction
{
    private readonly Position storehouse;

    public DepositWoodAction(Position storehouse)
    {
        this.storehouse = storehouse;
    }

    public override string Name => "DepositWood";

    public override bool CanRun(GoapState state)
    {
        var pos = state.Get<Position>("Position");
        var carried = state.GetOrDefault<int>("WoodCarried", 0);

        return pos.Equals(storehouse) && carried > 0;
    }

    public override void Apply(GoapState state)
    {
        var carried = state.GetOrDefault<int>("WoodCarried", 0);
        var stored = state.GetOrDefault<int>("StorehouseWood", 0);

        state.Set("WoodCarried", 0);
        state.Set("StorehouseWood", stored + carried);
    }

    public override int GetCost(GoapState state) => 1;

    public override void Execute(World world, Villager villager, ActionExecutionContext context)
    {
        var carried = context.PreState.Get<int>("WoodCarried");

        if (Source is StorehouseEntity storehouse)
            storehouse.AddWood(carried);
    }
}
