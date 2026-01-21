namespace VirtualVillage;

public class GoapAction
{
    public int Id { get; }
    public string Name { get; }
    public float Cost { get; }

    public Entity? Entity { get; }

    public Predicate<WorldState> Precondition { get; }
    public Action<WorldState> Effect { get; }

    public GoapAction(string name, float cost, Predicate<WorldState> precondition, Action<WorldState> effect, Entity? entity = null)
    {
        Id = IdGenerator.Next();
        Name = name;
        Cost = cost;
        Entity = entity;
        Precondition = precondition;
        Effect = effect;
    }

    public override string ToString() => Entity is null ? $"{Name} - cost {Cost}" : $"{Name} [{Entity}] - cost {Cost}";
}