using System.Windows.Media;
using Core;

namespace VirtualVillage;

public class Storehouse(Position position, Brush color) : IWorldObject
{
    public Position Position { get; set; } = position;
    public Brush Color { get; set; } = color;
    public Dictionary<string, int> Inventory { get; set; } = [];
}
