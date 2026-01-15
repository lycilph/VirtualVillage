namespace VirtualVillage.Actions;

public class DropOffWoodAction : GoapAction
{
    public DropOffWoodAction() : base("Drop off Wood")
    {
        Preconditions.Add("hasFirewood", true);
        Preconditions.Add("atStorehouse", true);

        Effects.Add("hasFirewood", false);
        Effects.Add("hasAxe", false);
        Effects.Add("deliveredWood", true);
    }

    public override void Execute(Villager agent)
    {
        agent.HasFirewood = false;
        Storehouse.Firewood++;
        Console.WriteLine($"{agent.Name} deposited firewood in the storehouse.");

        if (agent.HasAxe)
        {
            agent.HasAxe = false;
            Storehouse.Axes++;
            Console.WriteLine($"{agent.Name} returned axe to the storehouse.");
        }
    }
}