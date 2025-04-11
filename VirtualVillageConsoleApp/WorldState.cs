namespace VirtualVillageConsoleApp;

public class WorldState
{
    public Dictionary<string, int> states = [];

    public static WorldState Empty() => new();

    public int this[string key] => states[key];

    public void Set(string key, int value)
    {
        if (!states.TryAdd(key, value))
            states[key] = value;
    }

    public int Get(string key)
    {
        if (states.TryGetValue(key, out var value))
            return value;
        else
            throw new InvalidDataException($"World state doesn't contain the key: {key}");
    }
}
