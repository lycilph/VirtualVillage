public static class ResourceExtensions
{
    public static Dictionary<string, int> WithDelta(
        this Dictionary<string, int> source,
        string key,
        int delta)
    {
        var copy = new Dictionary<string, int>(source);
        copy[key] = copy.GetValueOrDefault(key) + delta;
        return copy;
    }
}