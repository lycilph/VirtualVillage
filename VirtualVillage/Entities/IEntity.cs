using VirtualVillage.Core;
using VirtualVillage.Domain;

namespace VirtualVillage.Entities;

public interface IEntity : IWorldObject, IActionProvider
{
    void Tick(World world);
}