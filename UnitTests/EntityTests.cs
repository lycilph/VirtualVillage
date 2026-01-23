using VirtualVillage;
using VirtualVillage.Entities;

namespace UnitTests;

public class EntityTests
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var entity = new Forest(new Location(1, 1), 5) as Entity;

        // Act
        var key = entity.GetStateKey("wood");

        // Assert
        Assert.Equal("Forest_1_wood", key);
    }
}
