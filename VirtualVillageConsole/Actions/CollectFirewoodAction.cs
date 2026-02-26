using VirtualVillageConsole.Core;

namespace VirtualVillageConsole.Actions;

public class CollectFirewoodAction(string name, float cost, int duration, IEntity entity) : GoapAction(name, cost, duration, entity)
{
}
