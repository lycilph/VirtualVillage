namespace VirtualVillage;

public class ChopWoodAction : GoapAction
{
    private readonly Position _forest;

    public ChopWoodAction(Position forest)
    {
        _forest = forest;
    }

    public override string Name => "ChopWood";

    public override bool CanRun(GoapState state)
        => state.Get<Position>("Position").Equals(_forest)
           && state.Get<int>("WoodCarried") < 5;

    public override void Apply(GoapState state)
    {
        state.Set("WoodCarried", state.Get<int>("WoodCarried") + 1);
    }

    public override int GetCost(GoapState state) => 2;
}
