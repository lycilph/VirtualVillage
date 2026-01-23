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

        return new Goal.Builder("Store Wood")
            .WithDesiredState(s => s.Get<int>(storehouse.GetStateKey("wood")) > 2)
            .Build();
    }

    public static Goal StoreOre(World world, Agent agent)
    {
        var storehouse = world.Entities
            .OfType<Storehouse>()
            .OrderBy(x => agent.Location.DistanceTo(x.Location))
            .First();

        return new Goal.Builder("Store Ore")
            .WithDesiredState(s => s.Get<int>(storehouse.GetStateKey("ore")) > 1)
            .Build();
    }

    public static Goal StoreAxe(World world, Agent agent)
    {
        var storehouse = world.Entities
            .OfType<Storehouse>()
            .OrderBy(x => agent.Location.DistanceTo(x.Location))
            .First();

        return new Goal.Builder("Store Axe")
            .WithDesiredState(s => s.Get<int>(storehouse.GetStateKey("axe")) > 0)
            .Build();
    }
}
