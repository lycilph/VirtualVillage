using VirtualVillage.Entities;

namespace VirtualVillage;

public class GoapAction(string name, float cost) : IdentifiableBase(name)
{
    public float Cost { get; } = cost;

    public Entity? Entity { get; private set; } = null;

    public HashSet<string> Tags { get; } = [];
    public Predicate<WorldState> Precondition { get; private set; } = s => false;
    public Action<WorldState> Effect { get; private set; } = s => { /* no-op */ };

    public Func<World, Agent, bool> CanExecute { get; private set; } = (w, a) => true;
    public Action<World, Agent> Execute { get; private set; } = (w, a) => { };
    public Func<World, Agent, bool> IsComplete { get; private set; } = (w, a) => true;

    public override string ToString() => Entity is null ? $"{Name}[{Id}] - cost {Cost}" : $"{Name}[{Id}] [{Entity}] - cost {Cost}";

    public class Builder(string name, float cost)
    {
        private readonly GoapAction action = new(name, cost);

        public Builder WithPrecondition(Predicate<WorldState> precondition)
        {
            action.Precondition = precondition;
            return this;
        }

        public Builder WithEffect(Action<WorldState> effect)
        {
            action.Effect = effect;
            return this;
        }

        public Builder WithEntity(Entity entity)
        {
            action.Entity = entity;
            return this;
        }

        public Builder WithTag(string tag)
        {
            action.Tags.Add(tag);
            return this;
        }

        public Builder WithExecution(Action<World, Agent> execute)
        {
            action.Execute = execute;
            return this;
        }

        public GoapAction Build() => action;
    }
}