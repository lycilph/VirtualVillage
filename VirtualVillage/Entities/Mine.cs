using VirtualVillage.Actions;
using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Planning;

namespace VirtualVillage.Entities;

public class Mine : WorldObject<Mine>, IEntity
{
    public int Ore { get; set; }

    private readonly List<GoapAction> actions = [];

    public Mine(Location location, int oreRemaining) : base("Mine", location)
    {
        Ore = oreRemaining;

        actions.Add(new MineOreAction(this, 5, 5));
    }

    public void Tick(World world) { }

    public override void Update(WorldState state)
    {
        state[GetStateKey(Keys.Ore)] = Ore;
    }

    public override void Render() => Console.WriteLine($"Mine @ {Location} (remaining ore: {Ore})");

    public IEnumerable<GoapAction> GetProvidedActions() => actions;
}
