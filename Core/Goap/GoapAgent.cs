using System.Diagnostics;
using System.Numerics;

namespace Core.Goap;

[DebuggerDisplay("Agent {Name} - {Position} - {CurrentAction}")]
public class GoapAgent<T> where T : GoapAction
{
    public Vector2 Position { get; set; } = Vector2.Zero;
    public string Name { get; set; } = string.Empty;

    public List<T> Actions { get; set; } = [];
    public List<GoapGoal> Goals { get; set; } = [];

    public GoapPlan<T>? CurrentPlan { get; set; } = null;
    public T? CurrentAction { get; set; } = null;
}
