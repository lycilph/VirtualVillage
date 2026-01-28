using VirtualVillage.Actions;
using VirtualVillage.Core;
using VirtualVillage.Planning;

namespace VirtualVillage.Entities;

public class Forge : WorldObject<Forge>, IEntity
{
    private readonly List<GoapAction> actions = [];

    public Forge(Location location) : base("Forge", location)
    {
        actions.Add(new CraftAxeAction(this, 5, 5));
    }

    public override void Update(WorldState state) { }
    
    public override void Render() => Console.WriteLine($"Forge @ {Location}");

    public IEnumerable<GoapAction> GetProvidedActions() => actions;
}
