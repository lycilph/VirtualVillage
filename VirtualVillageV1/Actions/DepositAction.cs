using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Entities;
using VirtualVillage.Planning;

namespace VirtualVillage.Actions;

public class DepositAction : GoapAction
{
    private readonly string value;

    public DepositAction(string value, float cost, int duration, IEntity entity) : base($"Deposit[{value}]", cost, duration, entity)
    {
        Tags.Add(Keys.AllJobs);
        this.value = value;
    }

    public override bool Precondition(WorldState state)
    {
        if (Entity == null) return false;
        if (Entity is not Storehouse storehouse) return false;

        return state.Get<Location>(Agent.GetGenericStateKey(Keys.Location)).DistanceTo(storehouse.Location) == 0 &&
               state.Has(Agent.GetGenericStateKey(value));
    }

    public override void Effect(WorldState state)
    {
        if (Entity == null) return;
        if (Entity is not Storehouse storehouse) return;

        state.Inc(storehouse.GetStateKey(value), 1);
        state.Dec(Agent.GetGenericStateKey(value), 1);
    }

    public override bool CanExecute(World world, Agent agent) => agent.Inventory.TryGetValue(value, out int resource) && resource > 0;

    public override void OnCompleted(World world, Agent agent)
    {
        if (Entity is not Storehouse storehouse) return;
        
        var resource = 0;

        if (storehouse.Inventory.TryGetValue(value, out resource))
            storehouse.Inventory[value] = resource + 1;
        else
            storehouse.Inventory[value] = 1;

        if (agent.Inventory.TryGetValue(value, out resource))
            agent.Inventory[value] = resource - 1;
    }
}
