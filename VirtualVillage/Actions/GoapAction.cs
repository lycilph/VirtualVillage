using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Entities;
using VirtualVillage.Planning;

namespace VirtualVillage.Actions;

public abstract class GoapAction(string name, float cost, int duration, IEntity? entity = null) : IdentifiableBase(name)
{
    public float Cost { get; } = cost;
    public int Duration { get; } = duration;
    public IEntity? Entity { get; private set; } = entity;
    public HashSet<string> Tags { get; } = [];

    // Planning related methods
    public virtual bool Precondition(WorldState state) => true;
    public virtual void Effect(WorldState state) { }

    // Execution related methods
    public ActionContext GetContext() => new(this);
    public virtual bool CanExecute(World world, Agent agent) => true;
    public virtual void Execute(World world, Agent agent, ActionContext context)
    {
        context.Tick();
        if (context.Elapsed == Duration)
            OnCompleted(world, agent);
    }
    public virtual void OnCompleted(World world, Agent agent) { }
    public virtual bool IsComplete(World world, Agent agent, ActionContext context) => context.Elapsed == Duration;

    public override string ToString() 
        => Entity is null ? 
           $"{Name}[{Id}] - cost {Cost}" : 
           $"{Name}[{Id}] [{Entity}] - cost {Cost}";
}