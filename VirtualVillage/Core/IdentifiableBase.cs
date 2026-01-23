namespace VirtualVillage.Core;

public class IdentifiableBase(string name) : IIdentifiable
{
    public int Id { get; } = IdGenerator.Next();
    public string Name { get; } = name;

    public override string ToString() => $"{Name}[{Id}]";
}
