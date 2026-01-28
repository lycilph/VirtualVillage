using VirtualVillage.Entities;
using VirtualVillage.Agents;
using VirtualVillage.Domain;

namespace VirtualVillage.Goals;

public static class GoalFactory
{
    public static Goal StoreWood(World world, Agent agent)
    {
        var storehouse = world.Entities
            .OfType<Storehouse>()
            .OrderBy(x => agent.Location.DistanceTo(x.Location))
            .First();

        if (!storehouse.Inventory.TryGetValue(Keys.Wood, out int wood))
            wood = 0;

        return new Goal.Builder("Store Wood")
            .WithDesiredState(s => s.Get<int>(storehouse.GetStateKey(Keys.Wood)) > wood)
            .WithPriority(s => wood < 5 ? 100 : 0)
            .Build();
    }

    public static Goal StoreOre(World world, Agent agent)
    {
        var storehouse = world.Entities
            .OfType<Storehouse>()
            .OrderBy(x => agent.Location.DistanceTo(x.Location))
            .First();

        if (!storehouse.Inventory.TryGetValue(Keys.Ore, out int ore))
            ore = 0;

        return new Goal.Builder("Store Ore")
            .WithDesiredState(s => s.Get<int>(storehouse.GetStateKey(Keys.Ore)) > ore)
            .WithPriority(s => ore < 5 ? 100 : 0)
            .Build();
    }

    public static Goal StoreAxe(World world, Agent agent)
    {
        var storehouse = world.Entities
            .OfType<Storehouse>()
            .OrderBy(x => agent.Location.DistanceTo(x.Location))
            .First();
        
        if (!storehouse.Inventory.TryGetValue(Keys.Axe, out int axe))
            axe = 0;

        return new Goal.Builder("Store Axe")
            .WithDesiredState(s => s.Get<int>(storehouse.GetStateKey(Keys.Axe)) > axe)
            .WithPriority(s => axe < 5 ? 100 : 0)
            .Build();
    }

    public static Goal Relax()
    {
        return new Goal.Builder("Relax")
            .WithDesiredState(s => s.Get<bool>(Agent.GetGenericStateKey(Keys.Relax)) == true)
            .WithPriority(s => 1)
            .Build();
    }
}
