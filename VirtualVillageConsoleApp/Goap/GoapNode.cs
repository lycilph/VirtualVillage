namespace VirtualVillageConsoleApp.Goap;

public class GoapNode(GoapNode? parent, double cost, Dictionary<string, object> state, GoapAction? action)
{
    public GoapNode? Parent { get; set; } = parent;
    public double RunningCost { get; set; } = cost;
    public Dictionary<string, object> State { get; set; } = state;
    public GoapAction? Action { get; set; } = action;
}
