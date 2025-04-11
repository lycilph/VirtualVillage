namespace VirtualVillageConsoleApp;

public class Simulation
{
    public List<Agent> Agents = [];
    public List<Item> Items = [];

    public void Update()
    {
        Agents.ForEach(a => a.Update());
    }

    public void Render()
    {
        Agents.ForEach(a => a.Render());
        Items.ForEach(i => i.Render());
    }
}
