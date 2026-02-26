namespace VirtualVillageConsole.Core;

public interface IActionProvider
{
    IEnumerable<GoapAction> GetProvidedActions();
}
