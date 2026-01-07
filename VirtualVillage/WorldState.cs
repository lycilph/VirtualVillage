namespace VirtualVillage;

public class WorldState
{
    public Dictionary<string, int> Values = [];
    public void Set(string key, int val) => Values[key] = val;
    public int Get(string key) => Values.TryGetValue(key, out int value) ? value : 0;
    public WorldState Clone() => new() { Values = new Dictionary<string, int>(Values) };
}
