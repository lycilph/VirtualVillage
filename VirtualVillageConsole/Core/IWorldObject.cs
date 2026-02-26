using VirtualVillageConsole.Planner;

namespace VirtualVillageConsole.Core;

public interface IWorldObject
{
    public int Id { get; }
    public string Name { get; }
    public Location Location { get; }

    public void Update(WorldState state);
    public void Tick();

    public string GetStateKey(string value);
    public static string GetGenericStateKey(string value) => throw new NotImplementedException();
}
