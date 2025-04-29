using System.Windows.Media;
using Core;

namespace VirtualVillage;

public class Tree(Position position, Brush color, int health) : IWorldObject
{
    public Position Position { get; set; } = position;
    public Brush Color { get; set; } = color;
    public int Health { get; set; } = health;
}
