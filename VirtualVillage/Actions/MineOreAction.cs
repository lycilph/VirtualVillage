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
        var pos = state.Get<Position>("Position");
        var carried = state.GetOrDefault<int>("OreCarried", 0);

        return pos.Equals(mine) && carried < 5;
    }

    public override void Apply(GoapState state)
    {
        var carried = state.GetOrDefault<int>("OreCarried", 0);
        state.Set("OreCarried", carried + 1);
    }

    public override int GetCost(GoapState state) => 3;
}
