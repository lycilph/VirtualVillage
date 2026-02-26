using VirtualVillage.Core;
using VirtualVillage.Domain;

namespace VirtualVillage.Entities;

public interface IEntity : IWorldObject, IActionProvider
{
    bool MustBeReserved { get; }
    int ReservedBy { get; set; }
    void Tick(World world);
}