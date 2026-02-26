using VirtualVillage.Planning;

namespace VirtualVillage.Core;

public abstract class WorldObject<T>(string name, Location location) : IWorldObject where T : WorldObject<T>
{
    public int Id { get; } = IdGenerator.Next();
    public string Name { get; } = name;
    public Location Location { get; set;  } = location;
    public int ReservedBy { get; set; } = -1; // -1 = unreserved

    public abstract void Update(WorldState state);
    public abstract void Render();

    public string GetStateKey(string value) => $"{Name}[{Id}]_{value}";
    public static string GetGenericStateKey(string value) => $"{typeof(T).Name}_{value}";

    public override string ToString() => $"{Name}[{Id}] {Location}";
}