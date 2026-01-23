using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Entities;

namespace UnitTests;

public class ActionTests
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var world = new World();
        world.Entities.Add(new Forest(new Location(5, 5), 5));
        world.Entities.Add(new Mine(new Location(-5, 2), 10));

        // Act
        var actions = world.GetActions();
        var move_to_forest_action = actions.Where(a => a.Name.Contains("forest", StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault();

        // Assert
        Assert.Equal(3, actions.Count);
        Assert.NotNull(move_to_forest_action);
    }
}
