using VirtualVillage.Entities;

namespace VirtualVillage.Actions;

public abstract class GoapAction
{
    public abstract string Name { get; }
    public WorldEntity Source { get; init; } = null!;

    public abstract bool CanRun(GoapState state);
    public abstract void Apply(GoapState state);

    public abstract int GetCost(GoapState state);

    // EXECUTION (default = no world effect)
    public virtual void Execute(World world, Villager villager, ActionExecutionContext context)
    {
        // Default: no side effects
    }

    // Optional spatial target
    public virtual bool HasTargetPosition => false;
    public virtual Position TargetPosition => default;
}