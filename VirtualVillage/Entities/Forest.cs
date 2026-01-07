namespace VirtualVillage.Entities;

public class Forest : IActionProvider
{
    public IEnumerable<GoapAction> GetActions()
    {
        var act = new GoapAction("Chop Wood", new Position(50, 20));
        act.Preconditions.Add(new StateRequirement { Key = "HasAxe", Value = 1, Condition = ConditionType.Equals });
        act.PredictedEffects.Add(new ActionEffect { Key = "Logs", Value = 1, IsRelative = true });
        yield return act;
    }
}