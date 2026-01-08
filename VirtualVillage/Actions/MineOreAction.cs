namespace VirtualVillage.Actions;

public class MineOreAction : GoapAction
{
    private readonly Position mine;

    public MineOreAction(Position mine)
    {
        this.mine = mine;
    }

    public override string Name => "MineOre";

    public override bool CanRun(GoapState state)
    {
        var pos = state.Get(WorldKeys.Position);
        var carried = state.GetOrDefault(WorldKeys.OreCarried, 0);

        return pos.Equals(mine) && carried < 5;
    }

    public override void Apply(GoapState state)
    {
        int carried = state.GetOrDefault(WorldKeys.OreCarried, 0);
        state.Set(WorldKeys.OreCarried, carried + 1);
    }

    public override int GetCost(GoapState state) => 3;
}
