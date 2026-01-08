namespace VirtualVillage;

public sealed class StateKey<T>
{
    public string Name { get; }

    private StateKey(string name) => Name = name;

    public override string ToString() => Name;

    public static StateKey<T> Create(string name) => new(name);
}
