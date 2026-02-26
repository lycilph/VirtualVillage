using VirtualVillage.Actions;
using VirtualVillage.Entities;
using VirtualVillage.Planning;

namespace VirtualVillage.Domain;

public class World
{
    private readonly MovementProvider movementProvider;
    private readonly DefaultActionsProvider defaultActionsProvider;


    public List<IEntity> Entities { get; } = [];
    public List<Agent> Agents { get; } = [];
    public List<string> Events { get; } = [];

    public World()
    {
        movementProvider = new MovementProvider(this);
        defaultActionsProvider = new DefaultActionsProvider();
    }

    public WorldState GetWorldState(Agent agent)
    {
        var state = new WorldState();
        
        agent.Update(state);

        foreach (var entity in Entities)
            entity.Update(state);

        return state;
    }

    public List<GoapAction> GetActions() => 
        [.. Entities.SelectMany(e => e.GetProvidedActions()), 
         .. movementProvider.GetProvidedActions(), 
         .. defaultActionsProvider.GetProvidedActions()];

    public void Tick()
    {
        Events.Clear();

        foreach (var agent in Agents)
            agent.Tick(this);

        foreach (var entity in Entities)
            entity.Tick(this);
    }

    public void Render()
    {
        Console.WriteLine("--- Agents ---");
        foreach (var agent in Agents)
            agent.Render();

        Console.WriteLine("--- Entities ---");
        foreach (var entity in Entities)
            entity.Render();

        Console.WriteLine("--- Events ---");
        foreach (var e in Events)
            Console.WriteLine(e);
    }
}
