using VirtualVillage.Agents;
using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Entities;
using VirtualVillage.Planning;

namespace VirtualVillage.Actions;

public abstract class GoapAction(string name, float cost, IEntity? entity = null) : IdentifiableBase(name)
{
    public float Cost { get; } = cost;
    public IEntity? Entity { get; private set; } = entity;
    public HashSet<string> Tags { get; } = [];

    public virtual bool Precondition(WorldState state) => true;
    public virtual void Effect(WorldState state) { }

    public virtual bool CanExecute(World world, Agent agent) => true;
    public virtual void Execute(World world, Agent agent) { }
    public virtual bool IsComplete(World world, Agent agent) => true;

    public override string ToString() 
        => Entity is null ? 
           $"{Name}[{Id}] - cost {Cost}" : 
           $"{Name}[{Id}] [{Entity}] - cost {Cost}";
}