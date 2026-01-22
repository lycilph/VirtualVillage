namespace VirtualVillage;

public class Agent(string name, Location location)
{
    public int Id { get; } = IdGenerator.Next();
    public string Name { get; } = name;
    public Location Location { get; } = location;

    public void Update(WorldState state)
    {
        state["agent_location"] = Location;
    }

    public override string ToString() => $"{Name}[{Id}] {Location}";
}
