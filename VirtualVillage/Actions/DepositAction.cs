using VirtualVillage.Agents;
using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Entities;
using VirtualVillage.Planning;

namespace VirtualVillage.Actions;

public class DepositAction : GoapAction
{
    private readonly string value;

    public DepositAction(string value, float cost, IEntity entity) : base($"Deposit[{value}]", cost, entity)
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
}
