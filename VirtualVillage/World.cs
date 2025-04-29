using System.Windows.Media;

namespace VirtualVillage;

public class World
{
    public List<Agent> Agents { get; set; } = [];
    public List<Tree> Trees { get; set; } = [];
    public Storehouse Store { get; set; } = new Storehouse(new Core.Position(0, 0), Brushes.Black);

    public List<IWorldObject> GetWorldObjects() => [.. Agents, .. Trees, Store];
}
