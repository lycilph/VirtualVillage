namespace VirtualVillage.Jobs;

public sealed class MinerJob : Job
{
    public MinerJob() : base("Miner") { }

    public override IEnumerable<Goal> GetGoals(World world, Agent agent)
    {
        yield return GoalFactory.StoreOre(world, agent);
    }

    public override bool AllowsAction(GoapAction action)
    {
        return action.Tags.Contains("All") ||
               action.Tags.Contains("Miner");
    }
}