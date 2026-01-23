using VirtualVillage.Entities;
using VirtualVillage.Domain;
using VirtualVillage.Core;
using VirtualVillage.Agents;
using VirtualVillage.Planning;

namespace VirtualVillage.Actions;

public class ChopWoodAction : GoapAction
{
    public ChopWoodAction(Forest forest, float cost) : base("Chop Wood", cost, forest)
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
}
