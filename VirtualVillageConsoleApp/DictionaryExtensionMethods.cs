namespace VirtualVillageConsoleApp;

public static class DictionaryExtensionMethods
{
    // Make a shallow clone of the dictionary
    public static Dictionary<string, bool> Clone(this Dictionary<string, bool> dictionary)
    {
        return dictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
    }

    public static bool HasState(this Dictionary<string, bool> dictionary, string key, object value)
    {
        return dictionary.TryGetValue(key, out var currentValue) && currentValue.Equals(value);
    }
}
