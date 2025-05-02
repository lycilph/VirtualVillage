namespace Core.Goap;

public static class DictionaryExtensionMethods
{
    // Make a shallow clone of the dictionary
    public static Dictionary<string, object> Clone(this Dictionary<string, object> dictionary)
    {
        return dictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
    }

    public static bool Has(this Dictionary<string, int> dictionary, string resource, int amount = 0) =>
        dictionary.TryGetValue(resource, out var value) && value >= amount;

    public static void AddResource(this Dictionary<string, int> dictionary, string resource, int amount = 1)
    {
        if (!dictionary.TryAdd(resource, amount))
            dictionary[resource] += amount;
    }

    public static void RemoveResource(this Dictionary<string, int> dictionary, string resource, int amount = 1)
    {
        if (dictionary.ContainsKey(resource))
            dictionary[resource] -= amount;
    }
}
