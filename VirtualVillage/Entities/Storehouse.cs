using VirtualVillage.Actions;
using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Planning;

namespace VirtualVillage.Entities;

public class Storehouse : WorldObject<Storehouse>, IEntity
{
    public Dictionary<string, int> Inventory { get; } = [];

    private readonly List<GoapAction> actions = [];

    public Storehouse(Location location) : base("Storehouse", location)
    {
        foreach (var item in Keys.Items)
        {
            actions.Add(new DepositAction(item, 1, 1, this));
            actions.Add(new PickupAction(item, 1, 1, this));
        }
    }

    public void Tick(World world) { }

    public override void Update(WorldState state) 
    {
        foreach (var kvp in Inventory)
        {
            if (kvp.Value > 0)
                state[GetStateKey(kvp.Key)] = kvp.Value;
        }
    }

    public override void Render()
    {
        var resources = string.Join(", ", Inventory.Select(kvp => $"{kvp.Key}: {kvp.Value}"));
        Console.WriteLine($"Storehouse @ {Location} ({resources})");
    }

    public IEnumerable<GoapAction> GetProvidedActions() => actions;
}
