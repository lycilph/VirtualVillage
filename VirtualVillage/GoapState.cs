namespace VirtualVillage;

public class GoapState
{
    private readonly Dictionary<string, object> values = [];

    public T Get<T>(string key) => (T)values[key];
    public bool Has(string key) => values.ContainsKey(key);
    public void Set(string key, object value) => values[key] = value;
    public IReadOnlyDictionary<string, object> GetAll() => values;
    
    public bool TryGet<T>(string key, out T value)
    {
        if (values.TryGetValue(key, out var v) && v is T typed)
        {
            value = typed;
            return true;
        }

        value = default!;
        return false;
    }

    public GoapState Clone()
    {
        var copy = new GoapState();
        foreach (var kv in values)
            copy.values[kv.Key] = kv.Value;
        return copy;
    }

    public bool Satisfies(GoapState goal)
    {
        foreach (var kv in goal.values)
        {
            if (!values.TryGetValue(kv.Key, out var val))
                return false;

            if (val is int current && kv.Value is int target)
            {
                if (current < target)
                    return false;
            }
            else if (!Equals(val, kv.Value))
            {
                return false;
            }
        }

        return true;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not GoapState other)
            return false;

        if (values.Count != other.values.Count)
            return false;

        foreach (var kv in values)
        {
            if (!other.values.TryGetValue(kv.Key, out var v))
                return false;

            if (!Equals(kv.Value, v))
                return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            foreach (var kv in values.OrderBy(k => k.Key))
            {
                hash = hash * 23 + kv.Key.GetHashCode();
                hash = hash * 23 + kv.Value.GetHashCode();
            }
            return hash;
        }
    }

}