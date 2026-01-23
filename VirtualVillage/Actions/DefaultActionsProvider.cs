using VirtualVillage.Domain;
using VirtualVillage.Entities;

namespace VirtualVillage.Actions;

public class DefaultActionsProvider : IActionProvider
{
    private readonly List<GoapAction> actions = [];

    public DefaultActionsProvider()
    {
        actions.Add(new ScavengeAction(Keys.Wood, Keys.Lumberjack, 50));
        actions.Add(new ScavengeAction(Keys.Ore, Keys.Miner, 50));
    }

    public IEnumerable<GoapAction> GetProvidedActions() => actions;
}
