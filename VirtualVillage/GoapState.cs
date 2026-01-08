namespace VirtualVillage;

public class GoapState
{
    private readonly Dictionary<object, object> values = [];

    public T Get<T>(StateKey<T> key) => (T)values[key];
    public bool Has<T>(StateKey<T> key) => values.ContainsKey(key);
    public void Set<T>(StateKey<T> key, T value) => values[key] = value!;
    public IReadOnlyDictionary<object, object> GetAll() => values;
    
    public bool TryGet<T>(StateKey<T> key, out T value)
    {
        if (values.TryGetValue(key, out var v) && v is T typed)
        {
            value = typed;
            return true;
        }

        value = default!;
        return false;
    }

    public T GetOrDefault<T>(StateKey<T> key, T defaultValue = default!)
    {
        if (values.TryGetValue(key, out var v) && v is T typed)
            return typed;

        return defaultValue;
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
            foreach (var (key, value) in values)
                hash ^= HashCode.Combine(key.GetHashCode(), value?.GetHashCode() ?? 0);
            return hash;
        }
    }
}