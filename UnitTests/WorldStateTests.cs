using VirtualVillage;

namespace UnitTests;

public class WorldStateTests
{
    [Fact]
    public void Test1()
    {
        // Arrange
        var state = new WorldState();
        state["location"] = new Location(1, 1);

        // Act
        var location = state.Get<Location>("location");
        var missing = state.Get<Location>("missing");

        // Assert
        Assert.Equal(new Location(1,1), location);
        Assert.Equal(new Location(0,0), missing);
    }

    [Fact]
    public void Test2()
    {
        // Arrange
        var state = new WorldState();
        state["ore"] = 1;
        state["stuff"] = 5;

        // Act
        state.Inc("wood", 1);
        state.Inc("ore", 1);
        state.Dec("stuff", 2);

        // Assert
        Assert.Equal(1, (int)state["wood"]);
        Assert.Equal(2, (int)state["ore"]);
        Assert.Equal(3, (int)state["stuff"]);
    }
}
