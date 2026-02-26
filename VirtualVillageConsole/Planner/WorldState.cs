using System.Text;

namespace VirtualVillageConsole.Planner;

public class WorldState : Dictionary<string, object>
{
    public WorldState() : base() { }

    // Copy constructor
    public WorldState(IDictionary<string, object> dictionary) : base(dictionary) { }

    // Note: If 'object' is a custom class, you'll need a deep copy logic.
    public WorldState Clone() => new(this);

    //public void Inc(string key, int delta) => this[key] = Get<int>(key) + delta;

    //public void Dec(string key, int delta) => this[key] = Get<int>(key) - delta;

    //public void Set(string key, object value) => this[key] = value;

    //public T Get<T>(string key) where T : struct
    //{
    //    if (TryGetValue(key, out object? value) && value is T typedValue)
    //        return typedValue;
    //    return default;
    //}

    //public bool Has(string key) => Get<int>(key) > 0;

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            foreach (var kvp in this)
            {
                // Key hash
                int kvpHash = kvp.Key.GetHashCode();
                // Value hash (handle nulls just in case)
                kvpHash ^= (kvp.Value?.GetHashCode() ?? 0);

                // Use addition so that the order of keys doesn't change the hash
                hash = hash + kvpHash;
            }
            return hash;
        }
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj)) return true;
        if (obj is not WorldState other || Count != other.Count) return false;

        foreach (var (key, value) in this)
        {
            if (!other.TryGetValue(key, out var otherValue) || !Equals(value, otherValue))
                return false;
        }
        return true;
    }
}