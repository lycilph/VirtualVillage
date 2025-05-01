using System.Numerics;

namespace VirtualVillage.Actions;

public class StoreResourceAction : ActionBase
{
    public StoreResourceAction(string resource, Vector2 position)
    {
        Name = $"Store Resource [{resource}]";
        Position = position;
        Cost = 1;
        Preconditions = new Dictionary<string, object> { { $"Has{resource}", true } };
        Effects = new Dictionary<string, object> { { $"Stored{resource}", true } };
    }
}
