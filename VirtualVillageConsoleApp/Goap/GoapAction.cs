using System.Diagnostics;

namespace VirtualVillageConsoleApp.Goap;

[DebuggerDisplay("Action {Name} - {Cost}")]
public class GoapAction
{
    public string Name { get; set; } = string.Empty;
    public double Cost { get; set; } = 0;
    public Dictionary<string, bool> Preconditions { get; set; } = [];
    public Dictionary<string, bool> Effects { get; set; } = [];
}