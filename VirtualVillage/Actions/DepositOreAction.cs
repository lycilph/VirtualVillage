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
        var pos = state.Get<Position>("Position");
        var carried = state.GetOrDefault<int>("OreCarried", 0);

        return pos.Equals(storehouse) && carried > 0;
    }

    public override void Apply(GoapState state)
    {
        var carried = state.GetOrDefault<int>("OreCarried", 0);
        var stored = state.GetOrDefault<int>("StorehouseOre", 0);

        state.Set("OreCarried", 0);
        state.Set("StorehouseOre", stored + carried);
    }

    public override int GetCost(GoapState state) => 1;

    public override void Execute(World world, Villager villager, ActionExecutionContext context)
    {
        var carried = context.PreState.Get<int>("OreCarried");

        if (Source is StorehouseEntity storehouse)
            storehouse.AddOre(carried);
    }
}
