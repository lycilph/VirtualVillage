using VirtualVillage.Actions;
using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Planning;

namespace VirtualVillage.Entities;

public class Forest : WorldObject<Forest>, IEntity
{
    public int Wood { get; set;  }

    private readonly List<GoapAction> actions = [];

    public Forest(Location location, int woodRemaining) : base("Forest", location)
    {
        Wood = woodRemaining;

        actions.Add(new ChopWoodAction(this, 5, 3));
    }

    public void Tick(World world) {}

    public override void Update(WorldState state)
    {
        state[GetStateKey(Keys.Wood)] = Wood;
    }

    public override void Render() => Console.WriteLine($"Forest @ {Location} (remaining wood: {Wood})");

    public IEnumerable<GoapAction> GetProvidedActions() => actions;
}
