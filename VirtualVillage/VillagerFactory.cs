using System.Numerics;
using VirtualVillage.Actions;
using VirtualVillage.Objects;

namespace VirtualVillage;

public static class VillagerFactory
{
    public static List<Villager> CreateVillages(World world)
    {
        return
            [
                CreateLumberjack(world)
            ];
    }

    public static Villager CreateLumberjack(World world)
    {
        var storehouse = world.Get<Storehouse>() ?? throw new InvalidDataException("No storehouse found");

        return new Villager
        {
            Name = "Lumberjack",
            Position = new Vector2(0, 0),
            Actions = [new ChopWoodAction(), new FindTwigsAction(), new StoreResourceAction(World.Wood, storehouse.Position), new GetResourceAction(World.Tools, storehouse.Position)],
            Goals = [ new() { Name = "Gather Wood", State = { { "StoredWood", true } } } ]
        };
    }
}
