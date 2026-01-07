namespace VirtualVillage;

public class ActionEffect
{
    public required string Key;
    public int Value;
    public bool IsRelative;

    public void Apply(WorldState state) => state.Set(Key, IsRelative ? state.Get(Key) + Value : Value);
}
