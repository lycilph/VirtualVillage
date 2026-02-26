using VirtualVillageConsole.Core;

namespace VirtualVillageConsole.Entities;

public class EntityBase<T>(string name, Location location) : WorldObject<T>(name, location), IEntity where T : WorldObject<T>
{
    public bool MustBeReserved { get; protected set; } = false;
    public int ReservedBy { get; set; } = -1;

    public virtual IEnumerable<GoapAction> GetProvidedActions() => [];
}
