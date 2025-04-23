using System.Text;

namespace VirtualVillageConsoleApp.Actions;

public class GoapAction(string name, float cost)
{
    public string Name { get; protected set; } = name;
    public float Cost { get; protected set; } = cost;
    public WorldState Preconditions { get; protected set; } = new();
    public WorldState Effects { get; protected set; } = new();

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Action: {Name} {Cost}");
        
        return sb.ToString();
    }
}
