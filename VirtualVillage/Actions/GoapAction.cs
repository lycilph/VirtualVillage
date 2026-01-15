using VirtualVillage.Core;

namespace VirtualVillage.Actions;

public abstract class GoapAction
{
    public string Name { get; protected set; }
    public float Cost { get; protected set; } = 1f;

    public WorldState Preconditions = [];
    public WorldState Effects = [];

    protected GoapAction(string name) { Name = name; }

    public virtual bool IsPossible(Villager agent) => true;
    public abstract void Execute(Villager agent);
    
    // This allows the planner to simulate the result of an action 
    // without actually changing the real world yet.
    public virtual void ApplyToState(WorldState state)
    {
        foreach (var effect in Effects)
            state[effect.Key] = effect.Value;
    }
}
