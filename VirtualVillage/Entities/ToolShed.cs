namespace VirtualVillage.Entities;

public class ToolShed : IActionProvider
{
    public IEnumerable<GoapAction> GetActions()
    {
        var act = new GoapAction("Collect Axe", new Position(10, 5));
        act.Preconditions.Add(new StateRequirement { Key = "HasAxe", Value = 0, Condition = ConditionType.Equals });
        act.Effects.Add(new ActionEffect { Key = "HasAxe", Value = 1, IsRelative = false });
        yield return act;
    }
}