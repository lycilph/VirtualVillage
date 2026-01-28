using VirtualVillage.Agents;
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
    public ExecutionContext GetContext() => new(this);
    public virtual bool CanExecute(World world, Agent agent) => true;
    public virtual void Execute(World world, Agent agent, ExecutionContext context) { }
    public virtual void OnCompleted(World world, Agent agent) { }
    public virtual bool IsComplete(World world, Agent agent, ExecutionContext context) => true;

    public override string ToString() 
        => Entity is null ? 
           $"{Name}[{Id}] - cost {Cost}" : 
           $"{Name}[{Id}] [{Entity}] - cost {Cost}";
}