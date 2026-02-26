namespace VirtualVillageConsole.Core;

public interface IEntity : IWorldObject, IActionProvider
{
    bool MustBeReserved { get; }
    int ReservedBy { get; set; }
}
