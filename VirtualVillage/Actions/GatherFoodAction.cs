namespace VirtualVillage.Actions;

public class GatherFoodAction : GoapAction
{
    private readonly Position forest;

    public GatherFoodAction(Position forest)
    {
        this.forest = forest;
    }

    public override string Name => "GatherFood";

    public override bool CanRun(GoapState state)
        => state.Get(WorldKeys.HasFood) == false
           && state.Get(WorldKeys.Position).Equals(forest);

    public override void Apply(GoapState state) => state.Set(WorldKeys.HasFood, true);

    public override int GetCost(GoapState state) => 3;
}