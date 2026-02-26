using VirtualVillage.Planning;

namespace VirtualVillage.Core;

public interface IWorldObject : IIdentifiable
{
    public Location Location { get; }

    public void Update(WorldState state);
    public void Render();

    public string GetStateKey(string value);
    public static string GetGenericStateKey(string value) => throw new NotImplementedException();
}
