namespace VirtualVillage;

public enum ConditionType { Equals, GreaterThan, LessThan }

public class StateRequirement
{
    public required string Key;
    public int Value;
    public ConditionType Condition;

    public bool IsMet(WorldState state)
    {
        int current = state.Get(Key);
        return Condition switch
        {
            ConditionType.Equals => current == Value,
            ConditionType.GreaterThan => current > Value,
            ConditionType.LessThan => current < Value,
            _ => false
        };
    }
}
