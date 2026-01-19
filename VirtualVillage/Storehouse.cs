namespace VirtualVillage;

public sealed class Storehouse : Entity
{
    public Storehouse(string id, Location location)
        : base(id, location) { }

    public override IEnumerable<GoapAction> GetActions(string agentId, WorldState state)
    {
        yield return new GoapAction(
            name: "DepositWood",
            cost: 1,
            targetEntityId: Id,
            precondition: s =>
            {
                var agent = s.Agents[agentId];
                return agent.Location == Location &&
                       agent.Energy >= 1 &&
                       agent.Inventory.GetValueOrDefault("Wood") > 0;
            },
            effect: s =>
            {
                var agent = s.Agents[agentId];
                var storehouse = s.Entities[Id];

                s.Agents[agentId] = agent with
                {
                    Inventory = agent.Inventory.WithDelta("Wood", -1),
                    Energy = agent.Energy - 1,
                };

                s.Entities[Id] = storehouse with
                {
                    Resources = storehouse.Resources.WithDelta("Wood", +1)
                };
            });
    }
}
