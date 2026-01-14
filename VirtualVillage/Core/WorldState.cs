namespace VirtualVillage.Core;

public class WorldState : Dictionary<string, bool>
{
    public bool IsMet(WorldState goal)
    {
        foreach (var condition in goal)
        {
            if (!ContainsKey(condition.Key) || this[condition.Key] != condition.Value)
                return false;
        }
        return true;
    }

    public WorldState Clone()
    {
        var clone = new WorldState();
        foreach (var kvp in this) clone.Add(kvp.Key, kvp.Value);
        return clone;
    }
}