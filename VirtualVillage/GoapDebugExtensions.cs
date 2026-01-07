namespace VirtualVillage;

public static class GoapDebugExtensions
{
    public static string ToDebugString(this GoapState state)
    {
        return string.Join(", ",
            state.GetAll().Select(kv => $"{kv.Key}={kv.Value}"));
    }
}
