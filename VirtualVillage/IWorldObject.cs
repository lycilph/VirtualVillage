using System.Windows.Media;
using Core;

namespace VirtualVillage;

public interface IWorldObject
{
    public Position Position { get; set; }
    public Brush Color { get; set; }
}
