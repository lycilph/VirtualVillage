using VirtualVillage.Actions;
using VirtualVillage.Domain;
using VirtualVillage.Goals;

namespace VirtualVillage.Jobs;

public sealed class LumberjackJob : Job
{
    public LumberjackJob() : base("Lumberjack") { }

    public override IEnumerable<Goal> GetGoals(World world, Agent agent)
    {
        yield return GoalFactory.StoreWood(world, agent);
        yield return GoalFactory.Relax();
    }

    public override bool AllowsAction(GoapAction action)
    {
        return action.Tags.Contains(Keys.AllJobs) ||
               action.Tags.Contains(Keys.Lumberjack);
    }
}