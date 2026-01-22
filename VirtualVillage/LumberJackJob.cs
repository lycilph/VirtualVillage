namespace VirtualVillage;

public sealed class LumberjackJob : Job
{
    public LumberjackJob() : base("Lumberjack") { }

    public override IEnumerable<Goal> GetGoals(World world, Agent agent)
    {
        yield return GoalFactory.StoreWood(world, agent);
    }

    public override bool AllowsAction(GoapAction action)
    {
        return action.Tags.Contains("Wood") ||
               action.Tags.Contains("Movement") ||
               action.Tags.Contains("Storage");
    }
}