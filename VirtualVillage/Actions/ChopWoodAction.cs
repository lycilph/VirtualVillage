namespace VirtualVillage.Actions;

public class ChopWoodAction : GoapAction
{
    public ChopWoodAction() : base("Chop Wood")
    {
        Preconditions.Add("atWoods", true);
        Preconditions.Add("hasAxe", true);
        Preconditions.Add("hasFirewood", false);

        Effects.Add("hasFirewood", true);
    }

    public override void Execute(Villager agent)
    {
        agent.HasFirewood = true;
        Console.WriteLine("Action: Chopping logs into firewood...");
    }
}