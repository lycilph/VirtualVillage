using VirtualVillage.Core;
using VirtualVillage.Entities;

namespace UnitTests;

public class EntityTests
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var entity = new Forest(new Location(1, 1), 5) as IEntity;

        // Act
        var key = entity.GetStateKey("wood");

        // Assert
        Assert.Equal("Forest[9]_wood", key);
    }
}
