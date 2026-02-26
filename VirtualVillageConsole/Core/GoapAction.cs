namespace VirtualVillageConsole.Core;

public abstract class GoapAction(string name, float cost, int duration, IEntity entity)
{
    public string Name { get; } = name;
    public float Cost { get; } = cost;
    public int Duration { get; } = duration;
    public IEntity Entity { get; protected set; } = entity;
    public HashSet<string> Roles { get; } = [];

    // Planning related methods
    //public virtual bool Precondition(WorldState state) => true;
    //public virtual void Effect(WorldState state) { }

    //// Execution related methods
    //public virtual bool CanExecute(World world, Agent agent) => true;
    //public virtual void Execute(World world, Agent agent, ActionContext context)
    //{
    //    context.Tick();
    //    if (context.Elapsed == Duration)
    //        OnCompleted(world, agent);
    //}
    //public virtual void OnCompleted(World world, Agent agent) { }
    //public virtual bool IsComplete(World world, Agent agent, ActionContext context) => context.Elapsed == Duration;
}