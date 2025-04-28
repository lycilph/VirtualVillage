using System.Windows.Media;
using Core;

namespace VirtualVillage;

public class Agent(Position position, Brush color)
{
    public Position Position { get; set; } = position;
    public Brush Color { get; set; } = color;
}
