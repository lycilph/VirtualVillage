using VirtualVillage.Actions;
using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Planning;

namespace VirtualVillage.Entities;

public class ScavengeOreLocation : WorldObject<ScavengeOreLocation>, IEntity
{
    public bool MustBeReserved { get; } = true;
    public int Amount { get; set; } = 1;

    private readonly GoapAction collectNuggetsAction;

    public ScavengeOreLocation(Location location) : base("Nugget", location)
    {
        collectNuggetsAction = new ScavengeAction(Keys.Ore, Keys.Miner, 50, 20, this);
    }

    public void Tick(World world)
    {
        if (Amount == 0)
        {
            Amount = 1;
            Location = Location.Random();
            world.Events.Add($"{Name} depleted, spawned new at {Location}");
        }
    }

    public override void Update(WorldState state)
    {
        state[GetStateKey(Keys.Ore)] = Amount;
    }

    public override void Render() => Console.WriteLine($"{Name} @ {Location} (remaining: {Amount})");

    public IEnumerable<GoapAction> GetProvidedActions() => ReservedBy == -1 ? [collectNuggetsAction] : [];

}
