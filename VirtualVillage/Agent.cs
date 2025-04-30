using System.Windows.Media;
using Core;
using Core.Goap;

namespace VirtualVillage;

public class Agent(Position position, Brush color, string name) : IWorldObject
{
    public Position Position { get; set; } = position;
    public Brush Color { get; set; } = color;
    public string Name { get; set; } = name;

    public Dictionary<string, int> Inventory { get; set; } = [];

    public List<GoapAction> Actions { get; set; } = [];
    public List<GoapGoal> Goals { get; set; } = [];

    public GoapPlan? CurrentPlan { get; set; } = null;
    public GoapAction? CurrentAction { get; set; } = null;
}
