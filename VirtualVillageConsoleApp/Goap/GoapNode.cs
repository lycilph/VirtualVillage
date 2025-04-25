namespace VirtualVillageConsoleApp.Goap;

public class GoapNode(GoapNode? parent, double cost, Dictionary<string, bool> state, GoapAction? action)
{
    public GoapNode? Parent { get; private set; } = parent;
    public double RunningCost { get; private set; } = cost;
    public Dictionary<string, bool> State { get; private set; } = state;
    public GoapAction? Action { get; private set; } = action;
}
