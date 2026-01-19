namespace VirtualVillage;

public sealed class Forest : Entity
{
    public Forest(string id, Location location)
        : base(id, location) { }

    public override IEnumerable<GoapAction> GetActions(string agentId, WorldState state)
    {
        yield return new GoapAction(
            name: "ChopTree",
            cost: 2,
            targetEntityId: Id,
            precondition: s =>
            {
                var agent = s.Agents[agentId];
                var forest = s.Entities[Id];

                return agent.Location == forest.Location &&
                       forest.Resources.GetValueOrDefault("Trees") > 0;
            },
            effect: s =>
            {
                var agent = s.Agents[agentId];
                var forest = s.Entities[Id];

                s.Entities[Id] = forest with
                {
                    Resources = forest.Resources.WithDelta("Trees", -1)
                };

                s.Agents[agentId] = agent with
                {
                    Inventory = agent.Inventory.WithDelta("Wood", +1)
                };
            });
    }
}
