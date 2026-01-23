using VirtualVillage.Agents;
using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Entities;
using VirtualVillage.Planning;

namespace VirtualVillage.Actions;

public class CraftAxeAction : GoapAction
{
    public CraftAxeAction(Forge forge, float cost) : base("Craft Axe", cost, forge)
    {
        Tags.Add(Keys.Blacksmith);
    }

    public override bool Precondition(WorldState state)
    {
        if (Entity == null) return false;
        if (Entity is not Forge forge) return false;

        return state.Get<Location>(Agent.GetGenericStateKey(Keys.Location)).DistanceTo(forge.Location) == 0 &&
               state.Has(Agent.GetGenericStateKey(Keys.Wood)) &&
               state.Has(Agent.GetGenericStateKey(Keys.Ore));
    }

    public override void Effect(WorldState state)
    {
        state.Inc(Agent.GetGenericStateKey(Keys.Axe), 1);
        state.Dec(Agent.GetGenericStateKey(Keys.Wood), 1);
        state.Dec(Agent.GetGenericStateKey(Keys.Ore), 1);
    }
}