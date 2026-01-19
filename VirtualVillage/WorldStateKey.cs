using VirtualVillage;

public static class WorldStateKey
{
    public static string Compute(WorldState state)
        => state.ToString();
}