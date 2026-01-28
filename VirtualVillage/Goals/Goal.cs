using VirtualVillage.Core;
using VirtualVillage.Planning;

namespace VirtualVillage.Goals;

public class Goal(string name) : IdentifiableBase(name)
{
    public Predicate<WorldState> IsValid { get; private set; } = s => true;
    public Func<WorldState, int> Priority { get; private set; } = s => 0;
    public Predicate<WorldState> DesiredState { get; private set; } = s => false;

    public class Builder(string name)
    {
        private readonly Goal goal = new(name);

        public Builder WithIsValid(Predicate<WorldState> valid)
        {
            goal.IsValid = valid;
            return this;
        }

        public Builder WithPriority(Func<WorldState, int> priority)
        {
            goal.Priority = priority;
            return this;
        }

        public Builder WithDesiredState(Predicate<WorldState> state)
        {
            goal.DesiredState = state;
            return this;
        }

        public Goal Build() => goal;
    }
}
