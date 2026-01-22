namespace VirtualVillage;

public abstract class Job(string name)
{
    public string Name { get; } = name;

    public abstract IEnumerable<Goal> GetGoals(World world, Agent agent);
    public abstract bool AllowsAction(GoapAction action);
}
