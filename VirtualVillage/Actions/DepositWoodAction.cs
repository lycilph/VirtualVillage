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
        var pos = state.Get(WorldKeys.Position);
        var carried = state.GetOrDefault(WorldKeys.WoodCarried, 0);

        return pos.Equals(storehouse) && carried > 0;
    }

    public override void Apply(GoapState state)
    {
        var carried = state.GetOrDefault(WorldKeys.WoodCarried, 0);
        var stored = state.GetOrDefault(WorldKeys.StorehouseWood, 0);

        state.Set(WorldKeys.WoodCarried, 0);
        state.Set(WorldKeys.StorehouseWood, stored + carried);
    }

    public override int GetCost(GoapState state) => 1;

    public override void Execute(World world, Villager villager, ActionExecutionContext context)
    {
        var carried = context.PreState.Get(WorldKeys.WoodCarried);

        if (Source is StorehouseEntity storehouse)
            storehouse.AddWood(carried);
    }
}
