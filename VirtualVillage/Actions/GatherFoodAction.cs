namespace VirtualVillage.Actions;

public class GatherFoodAction : GoapAction
{
    private readonly Position _forest;

    public GatherFoodAction(Position forest)
    {
        _forest = forest;
    }

    public override string Name => "GatherFood";

    public override bool CanRun(GoapState state)
        => state.Get<bool>("HasFood") == false
           && state.Get<Position>("Position").Equals(_forest);

    public override void Apply(GoapState state)
    {
        state.Set("HasFood", true);
    }

    public override int GetCost(GoapState state) => 3;
}