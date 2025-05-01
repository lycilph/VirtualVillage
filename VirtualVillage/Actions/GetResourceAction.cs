using System.Numerics;

namespace VirtualVillage.Actions;

public class GetResourceAction : ActionBase
{
    public GetResourceAction(string resource, Vector2 position)
    {
        Name = $"Get Resource [{resource}]";
        Position = position;
        Cost = 1;
        Preconditions = new Dictionary<string, object> { { $"Stored{resource}", true } };
        Effects = new Dictionary<string, object> { { $"Has{resource}", true } };
    }
}
