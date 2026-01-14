namespace VirtualVillage.Actions;

public class ChopWoodAction : GoapAction
{
    public ChopWoodAction() : base("Chop Wood")
    {
        Preconditions.Add("hasAxe", true);
        Effects.Add("hasFirewood", true);
    }

    public override void Execute(Villager agent) => Console.WriteLine("Action: Chopping logs into firewood...");
}