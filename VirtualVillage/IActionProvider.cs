namespace VirtualVillage;

public interface IActionProvider
{
    string Id { get; }

    IEnumerable<GoapAction> GetActions(string agentId, WorldState state);
}
