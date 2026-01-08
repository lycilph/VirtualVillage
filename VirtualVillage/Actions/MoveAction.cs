namespace VirtualVillage.Actions;

public class MoveAction(Position target) : GoapAction
{
    public override string Name => $"Move({target.X},{target.Y})";

    public override bool HasTargetPosition => true;
    public override Position TargetPosition => target;

    public override bool CanRun(GoapState state)
    {
        var pos = state.Get<Position>("Position");
        return !pos.Equals(target);
    }

    public override void Apply(GoapState state)
    {
        state.Set("Position", target);
    }

    public override int GetCost(GoapState state)
    {
        var pos = state.Get<Position>("Position");
        return pos.DistanceTo(target);
    }
}