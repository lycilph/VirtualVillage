using VirtualVillage.Entities;

namespace VirtualVillage.Actions;

public class DepositOreAction : GoapAction
{
    private readonly Position storehouse;

    public DepositOreAction(Position storehouse)
    {
        this.storehouse = storehouse;
    }

    public override string Name => "DepositOre";

    public override bool CanRun(GoapState state)
    {
        var pos = state.Get(WorldKeys.Position);
        var carried = state.GetOrDefault(WorldKeys.OreCarried, 0);

        return pos.Equals(storehouse) && carried > 0;
    }

    public override void Apply(GoapState state)
    {
        var carried = state.GetOrDefault(WorldKeys.OreCarried, 0);
        var stored = state.GetOrDefault(WorldKeys.StorehouseOre, 0);

        state.Set(WorldKeys.OreCarried, 0);
        state.Set(WorldKeys.StorehouseOre, stored + carried);
    }

    public override int GetCost(GoapState state) => 1;

    public override void Execute(World world, Villager villager, ActionExecutionContext context)
    {
        var carried = context.PreState.Get(WorldKeys.OreCarried);

        if (Source is StorehouseEntity storehouse)
            storehouse.AddOre(carried);
    }
}
