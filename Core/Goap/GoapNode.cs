namespace Core.Goap;

public class GoapNode<T>(GoapNode<T>? parent, double cost, Dictionary<string, object> state, T? action) where T : GoapAction
{
    public GoapNode<T>? Parent { get; set; } = parent;
    public double RunningCost { get; set; } = cost;
    public Dictionary<string, object> State { get; set; } = state;
    public T? Action { get; set; } = action;
}
