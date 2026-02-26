using VirtualVillage.Entities;

namespace VirtualVillage.Actions;

public class DefaultActionsProvider : IActionProvider
{
    private readonly List<GoapAction> actions = [];

    public DefaultActionsProvider()
    {
        actions.Add(new RelaxAction(1, 5));
    }

    public IEnumerable<GoapAction> GetProvidedActions() => actions;
}
