using System.Diagnostics;
using VirtualVillageConsoleApp.Simulation;

namespace VirtualVillageConsoleApp.Goap;

[DebuggerDisplay("Action {Name} - {Cost}")]
public class GoapAction
{
    public string Name { get; set; } = string.Empty;
    public double Cost { get; set; } = 0;
    public Position Position { get; set; } = new(0, 0);
    public Dictionary<string, object> Preconditions { get; set; } = [];
    public Dictionary<string, object> Effects { get; set; } = [];
}