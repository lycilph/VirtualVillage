using VirtualVillage.Actions;

namespace VirtualVillage.Entities;

public interface IActionProvider
{
    IEnumerable<GoapAction> GetProvidedActions();
}
