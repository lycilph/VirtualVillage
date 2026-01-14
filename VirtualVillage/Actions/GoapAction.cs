using VirtualVillage.Core;

namespace VirtualVillage.Actions;

public abstract class GoapAction
{
    public string Name { get; protected set; }
    public float Cost { get; protected set; } = 1f;

    public WorldState Preconditions = [];
    public WorldState Effects = [];

    protected GoapAction(string name) { Name = name; }

    public abstract void Execute(Villager agent);
    public virtual bool IsPossible(Villager agent) => true;
}
