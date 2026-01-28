using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Entities;
using VirtualVillage.Planning;

namespace VirtualVillage.Actions;

public class MineOreAction : GoapAction
{
    public MineOreAction(Mine mine, float cost, int duration) : base("Mine Ore", cost, duration, mine)
    {
        Tags.AddRange([Keys.AllJobs, Keys.Miner]);
    }

    public override bool Precondition(WorldState state)
    {
        if (Entity == null) return false;
        if (Entity is not Mine mine) return false;

        return state.Get<Location>(Agent.GetGenericStateKey(Keys.Location)).DistanceTo(mine.Location) == 0 &&
               state.Has(Agent.GetGenericStateKey(Keys.Pickaxe)) &&
               state.Has(mine.GetStateKey(Keys.Ore));
    }

    public override void Effect(WorldState state)
    {
        if (Entity == null) return;
        if (Entity is not Mine mine) return;

        state.Inc(Agent.GetGenericStateKey(Keys.Ore), 1);
        state.Dec(mine.GetStateKey(Keys.Ore), 1);
    }

    public override bool CanExecute(World world, Agent agent) => Entity is Mine mine && mine.Ore >= 0;

    public override void OnCompleted(World world, Agent agent)
    {
        if (Entity is not Mine mine) return;

        mine.Ore -= 1;

        if (agent.Inventory.TryGetValue(Keys.Ore, out var ore))
            agent.Inventory[Keys.Ore] = ore + 1;
        else
            agent.Inventory[Keys.Ore] = 1;
    }
}
