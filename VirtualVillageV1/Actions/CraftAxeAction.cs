using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Entities;
using VirtualVillage.Planning;

namespace VirtualVillage.Actions;

public class CraftAxeAction : GoapAction
{
    public CraftAxeAction(Forge forge, float cost, int duration) : base("Craft Axe", cost, duration, forge)
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

    public override bool CanExecute(World world, Agent agent) => 
        Entity is Forge &&
        agent.Inventory.TryGetValue(Keys.Wood, out int wood) && wood > 0 &&
        agent.Inventory.TryGetValue(Keys.Ore, out int ore) && ore > 0;

    public override void OnCompleted(World world, Agent agent)
    {
        if (agent.Inventory.TryGetValue(Keys.Wood, out var wood))
            agent.Inventory[Keys.Wood] = wood - 1;

        if (agent.Inventory.TryGetValue(Keys.Ore, out var ore))
            agent.Inventory[Keys.Ore] = ore - 1;

        if (agent.Inventory.TryGetValue(Keys.Axe, out var axe))
            agent.Inventory[Keys.Axe] = axe + 1;
        else
            agent.Inventory[Keys.Axe] = 1;
    }
}