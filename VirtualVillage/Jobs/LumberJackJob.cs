namespace VirtualVillage.Jobs;

public sealed class LumberjackJob : Job
{
    public LumberjackJob() : base("Lumberjack") { }

    public override IEnumerable<Goal> GetGoals(World world, Agent agent)
    {
        yield return GoalFactory.StoreWood(world, agent);
    }

    public override bool AllowsAction(GoapAction action)
    {
        return action.Tags.Contains("All") ||
               action.Tags.Contains("Lumberjack");
    }
}