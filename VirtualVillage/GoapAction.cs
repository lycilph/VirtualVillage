namespace VirtualVillage;

public class GoapAction
{
    public string Name { get; }
    public float Cost { get; }

    public string? TargetEntityId { get; }

    public Predicate<WorldState> Precondition { get; }
    public Action<WorldState> Effect { get; }

    public GoapAction(
        string name,
        float cost,
        Predicate<WorldState> precondition,
        Action<WorldState> effect,
        string? targetEntityId = null)
    {
        Name = name;
        Cost = cost;
        TargetEntityId = targetEntityId;
        Precondition = precondition;
        Effect = effect;
    }

    public override string ToString()
        => TargetEntityId is null
            ? Name
            : $"{Name} [{TargetEntityId}]";
}
