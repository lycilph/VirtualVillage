using VirtualVillageConsole.Core;

namespace VirtualVillageConsole.Entities;

public class CampFire : EntityBase<CampFire>
{
    public CampFire(Location location) : base("Camp Fire", location)
    {
    }
}
