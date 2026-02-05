using VirtualVillage.Actions;
using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Planning;

namespace VirtualVillage.Entities;

public class ScavengeWoodLocation : WorldObject<ScavengeWoodLocation>, IEntity
{
    public int Amount { get; set; } = 1;
    
    private readonly List<GoapAction> actions = [];

    public ScavengeWoodLocation(Location location) : base("Twigs", location)
    {
        actions.Add(new ScavengeAction(Keys.Wood, Keys.Lumberjack, 50, 20, this));
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

    public override void Render() => Console.WriteLine($"{Name} @ {Location} (remaining: {Amount})");

    public IEnumerable<GoapAction> GetProvidedActions() => actions;
}
