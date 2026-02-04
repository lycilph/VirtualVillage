using VirtualVillage.Actions;
using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Planning;

namespace VirtualVillage.Entities;

public class ScavengeWoodLocation(Location location) : WorldObject<ScavengeWoodLocation>("Twigs", location), IEntity
{
    public int Amount { get; set; } = 1;

    public override void Update(WorldState state)
    {
        state[GetStateKey(Keys.Wood)] = Amount;
    }

    public override void Render() => Console.WriteLine($"{Name} @ {Location} (remaining: {Amount})");

    public IEnumerable<GoapAction> GetProvidedActions() => [new ScavengeAction(Keys.Wood, Keys.Lumberjack, 50, 20, this)];
}
