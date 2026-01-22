namespace VirtualVillage;

public abstract class IdentifiableBase(string name) : IIdentifiable
{
    public int Id { get; } = IdGenerator.Next();
    public string Name { get; } = name;

    public string GetStateKey(string value) => $"{Name}[{Id}]_{value}";

    public override string ToString() => $"{Name}[{Id}]";
}
