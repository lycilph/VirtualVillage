namespace VirtualVillage;

public interface IActionProvider
{
    IEnumerable<GoapAction> GetProvidedActions();
}
