using VirtualVillage.Entities;
using VirtualVillage.Domain;
using VirtualVillage.Core;
using VirtualVillage.Planning;

namespace VirtualVillage.Actions;

public class ChopWoodAction : GoapAction
{
    public ChopWoodAction(Forest forest, float cost, int duration) : base("Chop Wood", cost, duration, forest)
    {
        Tags.AddRange([Keys.AllJobs, Keys.Lumberjack]);
    }

    public override bool Precondition(WorldState state)
    {
        if (Entity == null) return false;
        if (Entity is not Forest forest) return false;

        return state.Get<Location>(Agent.GetGenericStateKey(Keys.Location)).DistanceTo(forest.Location) == 0 &&
               state.Has(Agent.GetGenericStateKey(Keys.Axe)) &&
               state.Has(forest.GetStateKey(Keys.Wood));
    }

    public override void Effect(WorldState state)
    {
        if (Entity == null) return;
        if (Entity is not Forest forest) return;

        state.Inc(Agent.GetGenericStateKey(Keys.Wood), 1);
        state.Dec(forest.GetStateKey(Keys.Wood), 1);
    }

    public override bool CanExecute(World world, Agent agent) => Entity is Forest forest && forest.Wood > 0;
    
    public override void OnCompleted(World world, Agent agent)
    {
        if (Entity is not Forest forest) return;

        forest.Wood -= 1;

        if (agent.Inventory.TryGetValue(Keys.Wood, out var wood))
            agent.Inventory[Keys.Wood] = wood + 1;
        else
            agent.Inventory[Keys.Wood] = 1;
    }
}
