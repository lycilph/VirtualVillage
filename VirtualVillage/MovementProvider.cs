namespace VirtualVillage;

public sealed class MovementProvider : IActionProvider
{
    public string Id => "Movement";

    public IEnumerable<GoapAction> GetActions(string agentId, WorldState state)
    {
        foreach (var entity in state.Entities.Values)
        {
            yield return new GoapAction(
                name: "MoveTo " + entity.Location,
                cost: 1,
                targetEntityId: entity.Id,
                precondition: s =>
                {
                    var agent = s.Agents[agentId];

                    return agent.Location != entity.Location && 
                           agent.Energy >= 1;
                },
                effect: s =>
                {
                    var agent = s.Agents[agentId];
                    s.Agents[agentId] = agent with
                    {
                        Location = entity.Location,
                        Energy = agent.Energy - 1,
                    };
                });
        }
    }
}
