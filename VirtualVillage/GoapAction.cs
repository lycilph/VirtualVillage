namespace VirtualVillage;

public class GoapAction
{
    public string Name { get; }
    public float Cost { get; }
    public Predicate<WorldState> Precondition { get; }
    public Action<WorldState> Effect { get; }


    public GoapAction(string name, float cost, Predicate<WorldState> pre, Action<WorldState> effect)
    {
        Name = name;
        Cost = cost;
        Precondition = pre;
        Effect = effect;
    }
}