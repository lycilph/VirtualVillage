using VirtualVillage.Actions;
using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Planning;

namespace VirtualVillage.Entities;

public class Mine : WorldObject<Mine>, IEntity
{
    public bool MustBeReserved { get; } = true;
    public int Ore { get; set; }

    private readonly GoapAction mineAction;

    public Mine(Location location, int oreRemaining) : base("Mine", location)
    {
        Ore = oreRemaining;

        mineAction = new MineOreAction(this, 5, 5);
    }

    public void Tick(World world) { }

    public override void Update(WorldState state)
    {
        state[GetStateKey(Keys.Ore)] = Ore;
    }

    public override void Render() => Console.WriteLine($"Mine @ {Location} (remaining ore: {Ore})");

    public IEnumerable<GoapAction> GetProvidedActions() => ReservedBy == -1 ? [mineAction] : [];
}
