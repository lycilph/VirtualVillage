namespace VirtualVillage;

public abstract class Entity : IActionProvider
{
    public string Id { get; }
    public Location Location { get; }

    protected Entity(string id, Location location)
    {
        Id = id;
        Location = location;
    }

    public abstract IEnumerable<GoapAction> GetActions(string agentId, WorldState state);
}
