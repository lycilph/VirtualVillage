namespace VirtualVillage;

public class WorldState : Dictionary<string, object>
{
    public WorldState() : base() { }

    // Copy constructor
    public WorldState(IDictionary<string, object> dictionary) : base(dictionary) { }

    // Note: If 'object' is a custom class, you'll need a deep copy logic.
    public WorldState Clone() => new(this);

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
        if (obj == null) return false;
        
        if (ReferenceEquals(this, obj)) return true;

        if (obj is not WorldState other || Count != other.Count)
            return false;

        foreach (var kvp in this)
        {
            if (!other.TryGetValue(kvp.Key, out object? otherValue))
                return false;

            // Use Equals() to compare the content of the objects, not just references
            if (kvp.Value == null)
            {
                if (otherValue != null) return false;
            }
            else if (!kvp.Value.Equals(otherValue))
            {
                return false;
            }
        }
        return true;
    }
}
