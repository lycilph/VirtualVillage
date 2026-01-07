namespace VirtualVillage;

public class MineOreAction : GoapAction
{
    private readonly Position _mine;

    public MineOreAction(Position mine)
    {
        _mine = mine;
    }

    public override string Name => "MineOre";

    public override bool CanRun(GoapState state)
        => state.Get<Position>("Position").Equals(_mine)
           && state.Get<int>("OreCarried") < 5;

    public override void Apply(GoapState state)
    {
        state.Set("OreCarried", state.Get<int>("OreCarried") + 1);
    }

    public override int GetCost(GoapState state) => 3;
}
