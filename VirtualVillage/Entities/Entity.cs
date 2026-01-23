namespace VirtualVillage.Entities;

public abstract class Entity(string name, Location location) : IdentifiableBase(name), IActionProvider
{
    public Location Location { get; } = location;

    public abstract IEnumerable<GoapAction> GetProvidedActions();

    public abstract void Update(WorldState state);

    public abstract void Render();

    public override string ToString() => $"{Name}[{Id}] {Location}";
}
