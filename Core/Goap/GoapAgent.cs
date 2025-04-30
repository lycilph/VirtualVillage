using System.Diagnostics;
using System.Numerics;

namespace Core.Goap;

[DebuggerDisplay("Agent {Name} - {Position} - {CurrentAction}")]
public class GoapAgent
{
    public Vector2 Position { get; set; } = Vector2.Zero;
    public string Name { get; set; } = string.Empty;

    public Dictionary<string, int> Inventory { get; set; } = [];

    public List<GoapAction> Actions { get; set; } = [];
    public List<GoapGoal> Goals { get; set; } = [];

    public GoapPlan? CurrentPlan { get; set; } = null;
    public GoapAction? CurrentAction { get; set; } = null;
}
