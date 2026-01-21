namespace VirtualVillage;

public class Goal(string name, Predicate<WorldState> desiredState)
{
    public int Id { get; } = IdGenerator.Next();
    public string Name { get; } = name;
    public Predicate<WorldState> DesiredState { get; } = desiredState;
}
