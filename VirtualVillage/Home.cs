namespace VirtualVillage;

public sealed class Home : Entity
{
    public Home(string id, Location location)
         : base(id, location) { }

    public override IEnumerable<GoapAction> GetActions(string agentId, WorldState state)
    {
        var agent = state.Agents[agentId];

        yield return new GoapAction(
            name: "Rest",
            cost: 1,
            targetEntityId: Id,
            precondition: s =>
            {
                var agent = s.Agents[agentId];
                var home = s.Entities[Id];

                return agent.Energy < agent.MaxEnergy &&
                       agent.Location == home.Location;
            },
            effect: s =>
            {
                var a = s.Agents[agentId];
                s.Agents[agentId] = a with
                {
                    Energy = Math.Min(a.Energy + 5, a.MaxEnergy)
                };
            });
    }
}