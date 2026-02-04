using VirtualVillage.Actions;
using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Planning;

namespace VirtualVillage.Entities;

public class ScavengeOreLocation(Location location) : WorldObject<ScavengeOreLocation>("Nugget", location), IEntity
{
    public int Amount { get; set; } = 1;

    public override void Update(WorldState state)
    {
        state[GetStateKey(Keys.Ore)] = Amount;
    }

    public override void Render() => Console.WriteLine($"{Name} @ {Location} (remaining: {Amount})");

    public IEnumerable<GoapAction> GetProvidedActions() => [new ScavengeAction(Keys.Ore, Keys.Miner, 50, 20, this)];

}
