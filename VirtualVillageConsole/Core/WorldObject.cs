using VirtualVillageConsole.Misc;
using VirtualVillageConsole.Planner;

namespace VirtualVillageConsole.Core;

public abstract class WorldObject<T>(string name, Location location) : IWorldObject where T : WorldObject<T>
{
    public int Id { get; } = IdGenerator.Next();
    public string Name { get; } = name;
    public Location Location { get; set;  } = location;

    public virtual void Update(WorldState state) { }
    public virtual void Tick() {}

    public string GetStateKey(string value) => $"{Name}[{Id}]_{value}";
    public static string GetGenericStateKey(string value) => $"{typeof(T).Name}_{value}";

}