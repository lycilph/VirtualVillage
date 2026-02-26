using VirtualVillage.Actions;
using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Planning;

namespace VirtualVillage.Entities;

public class Forest : WorldObject<Forest>, IEntity
{
    public bool MustBeReserved { get; } = true;
    public int Wood { get; set;  }

    private readonly GoapAction chopAction;

    public Forest(Location location, int woodRemaining) : base("Forest", location)
    {
        Wood = woodRemaining;
        chopAction = new ChopWoodAction(this, 5, 3);
    }

    public void Tick(World world) {}

    public override void Update(WorldState state)
    {
        state[GetStateKey(Keys.Wood)] = Wood;
    }

    public override void Render() => Console.WriteLine($"Forest @ {Location} (remaining wood: {Wood}, reserved by: {ReservedBy})");

    public IEnumerable<GoapAction> GetProvidedActions() => ReservedBy == -1 ? [chopAction] : [];
}
