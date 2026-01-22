
namespace VirtualVillage;

public class DefaultActionsProvider : IActionProvider
{
    private readonly List<GoapAction> actions = [];

    public DefaultActionsProvider()
    {
        actions.Add(
            new GoapAction.Builder("Scavenge Wood", 50)
            .WithPrecondition(s => true)
            .WithEffect(s => s.Inc("agent_wood", 1))
            .Build());

        actions.Add(
            new GoapAction.Builder("Scavenge Ore", 50)
            .WithPrecondition(s => true)
            .WithEffect(s => s.Inc("agent_ore", 1))
            .Build());
    }

    public IEnumerable<GoapAction> GetProvidedActions() => actions;
}
