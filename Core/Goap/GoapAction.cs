using System.Diagnostics;
using System.Numerics;

namespace Core.Goap;

[DebuggerDisplay("Action {Name} - {Cost}")]
public class GoapAction
{
    public Vector2 Position { get; set; } = new(0, 0);
    public string Name { get; set; } = string.Empty;

    public double Cost { get; set; } = 0;
    public int Duration { get; set; } = 0;

    public Dictionary<string, object> Preconditions { get; set; } = [];
    public Dictionary<string, object> Effects { get; set; } = [];
}