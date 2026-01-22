namespace VirtualVillage;

public class Goal(string name) : IdentifiableBase(name)
{
    public Predicate<WorldState> DesiredState { get; private set; } = s => false;

    public class Builder(string name)
    {
        private readonly Goal goal = new(name);

        public Builder WithDesiredState(Predicate<WorldState> state)
        {
            goal.DesiredState = state;
            return this;
        }

        public Goal Build() => goal;
    }
}
