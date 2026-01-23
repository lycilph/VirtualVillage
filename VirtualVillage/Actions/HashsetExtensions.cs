namespace VirtualVillage.Actions;

public static class HashsetExtensions
{
    public static void AddRange<T>(this HashSet<T> set, IEnumerable<T> values) => set.UnionWith(values);
}
