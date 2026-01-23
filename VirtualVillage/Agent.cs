using VirtualVillage.Jobs;

namespace VirtualVillage;

public class Agent(string name, Job job, Location location)
{
    public int Id { get; } = IdGenerator.Next();
    public string Name { get; } = name;
    public Location Location { get; } = location;
    public Job Job { get; } = job;

    public void Update(WorldState state)
    {
        state["agent_location"] = Location;
    }

    public override string ToString() => $"{Name}[{Id}] {Location}";
}
