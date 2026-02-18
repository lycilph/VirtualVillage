using VirtualVillage.Actions;
using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Planning;

namespace VirtualVillage.Entities;

public class ScavengeWoodLocation : WorldObject<ScavengeWoodLocation>, IEntity
{
    public bool MustBeReserved { get; } = true;
    public int Amount { get; set; } = 1;

    private readonly GoapAction collectTwigsAction;

    public ScavengeWoodLocation(Location location) : base("Twigs", location)
    {
        collectTwigsAction = new ScavengeAction(Keys.Wood, Keys.Lumberjack, 50, 20, this);
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
        state[GetStateKey(Keys.Wood)] = Amount;
    }

    public override void Render() => Console.WriteLine($"{Name} @ {Location} (remaining: {Amount}, reserved by: {ReservedBy})");

    public IEnumerable<GoapAction> GetProvidedActions() => ReservedBy == -1 ? [collectTwigsAction] : [];
}
