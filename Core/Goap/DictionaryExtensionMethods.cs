namespace Core.Goap;

public static class DictionaryExtensionMethods
{
    // Make a shallow clone of the dictionary
    public static Dictionary<string, object> Clone(this Dictionary<string, object> dictionary)
    {
        return dictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
    }
}
