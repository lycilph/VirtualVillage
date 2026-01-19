using VirtualVillage;

public sealed class PlanNode
{
    public WorldState State { get; }
    public PlanNode? Parent { get; }
    public GoapAction? Action { get; }

    public float G { get; } // cost so far
    public float H { get; } // heuristic
    public float F => G + H;

    public PlanNode(
        WorldState state,
        PlanNode? parent,
        GoapAction? action,
        float g,
        float h)
    {
        State = state;
        Parent = parent;
        Action = action;
        G = g;
        H = h;
    }
}