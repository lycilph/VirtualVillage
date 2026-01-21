
namespace VirtualVillage;

public class Storehouse(Location location) : Entity("Storehouse", location)
{
    public override IEnumerable<GoapAction> GetProvidedActions()
    {
        yield return new GoapAction("Get Axe", 1, s => true, s => { }, this);
        yield return new GoapAction("Deposit Wood", 1, s => true, s => { }, this);
    }
}
