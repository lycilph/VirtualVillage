namespace VirtualVillage.Actions;

public class PickUpAxeAction : GoapAction
{
    public PickUpAxeAction() : base("Pick up Axe")
    {
        Preconditions.Add("atStorehouse", true);
        Preconditions.Add("hasAxe", false);

        Effects.Add("hasAxe", true);
    }

    public override bool IsPossible(Villager agent) => Storehouse.Axes > 0;

    public override void Execute(Villager agent)
    {
        Storehouse.Axes--;
        agent.HasAxe = true;
        Console.WriteLine($"{agent.Name} took an axe from the storehouse.");
    }
}