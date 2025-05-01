using VirtualVillage.Objects;

namespace VirtualVillage;

public static class WorldFactory
{
    public static World CreateWorld()
    {
        var world = new World();

        var storehouse = new Storehouse();
        storehouse.Inventory.Add("Tools", 1);
        world.WorldObjects.Add(storehouse);

        for (int i = 0; i < 5; i++)
            world.WorldObjects.Add(new Tree(world.GetRandomPosition()));

        for (int i = 0; i < 3; i++)
            world.WorldObjects.Add(new Twigs(world.GetRandomPosition()));

        world.Villagers = VillagerFactory.CreateVillages(world);

        return world;
    }
}
