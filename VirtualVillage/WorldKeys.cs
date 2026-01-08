namespace VirtualVillage;

public static class WorldKeys
{
    public static readonly StateKey<int> OreCarried =
        StateKey<int>.Create("OreCarried");

    public static readonly StateKey<int> WoodCarried =
        StateKey<int>.Create("WoodCarried");

    public static readonly StateKey<int> StorehouseOre =
        StateKey<int>.Create("StorehouseOre");

    public static readonly StateKey<int> StorehouseWood =
        StateKey<int>.Create("StorehouseWood");

    public static readonly StateKey<Position> Position =
        StateKey<Position>.Create("Position");

    public static readonly StateKey<bool> HasFood =
        StateKey<bool>.Create("HasFood");
}
