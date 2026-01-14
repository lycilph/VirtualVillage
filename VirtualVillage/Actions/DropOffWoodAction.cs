namespace VirtualVillage.Actions;

public class DropOffWoodAction : GoapAction
{
    public DropOffWoodAction() : base("Drop off Wood")
    {
        Preconditions.Add("hasFirewood", true);
        Effects.Add("deliveredWood", true);
    }

    public override void Execute(Villager agent)
    {
        agent.CurrentState["hasFirewood"] = false;
        Storehouse.Firewood++;
        Console.WriteLine($"{agent.Name} deposited firewood in the storehouse.");

        agent.CurrentState["hasAxe"] = false;
        Storehouse.Axes++;
        Console.WriteLine($"{agent.Name} returned axes to the storehouse.");
    }
}