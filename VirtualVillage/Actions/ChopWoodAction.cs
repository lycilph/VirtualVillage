namespace VirtualVillage.Actions;

public class ChopWoodAction : GoapAction
{
    private readonly Position forest;

    public ChopWoodAction(Position forest)
    {
        this.forest = forest;
    }

    public override string Name => "ChopWood";

    public override bool CanRun(GoapState state)
    {
        var pos = state.GetOrDefault(WorldKeys.Position);
        var carried = state.GetOrDefault(WorldKeys.WoodCarried, 0);

        return pos.Equals(forest) && carried < 5;
    }

    public override void Apply(GoapState state)
    {
        var carried = state.GetOrDefault(WorldKeys.WoodCarried, 0);
        state.Set(WorldKeys.WoodCarried, carried + 1);
    }

    public override int GetCost(GoapState state) => 2;
}
