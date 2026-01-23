namespace VirtualVillage.Jobs;

public sealed class BlacksmithJob : Job
{
    public BlacksmithJob() : base("Blacksmith") { }

    public override IEnumerable<Goal> GetGoals(World world, Agent agent)
    {
        yield return GoalFactory.StoreAxe(world, agent);
    }

    public override bool AllowsAction(GoapAction action)
    {
        return action.Tags.Contains("All") ||
               action.Tags.Contains("Blacksmith");
    }
}