using VirtualVillage.Actions;
using VirtualVillage.Agents;
using VirtualVillage.Core;
using VirtualVillage.Domain;
using VirtualVillage.Entities;
using VirtualVillage.Jobs;

namespace UnitTests;

public class ActionDurationTest
{
    private World world;
    private Agent agent;
    private Forest forest;
    private ChopWoodAction chop;
    private MoveToAction move;

    public ActionDurationTest()
    {
        world = new World();
        agent = new Agent("Bob", new LumberjackJob(), new Location(0, 0));
        
        forest = new Forest(new Location(3, 3), 5);
        chop = new ChopWoodAction(forest, 1, 5);

        move = new MoveToAction(forest, 5);
    }

    [Fact]
    public void ChopWoodDurationTest1()
    {
        // Arrange
        // See constructor

        // Act
        var ctx = chop.GetContext();
        chop.Execute(world, agent, ctx);

        // Assert
        Assert.Equal(1, ctx.Elapsed);
        Assert.Equal(5, forest.Wood);
        Assert.False(agent.Inventory.ContainsKey(Keys.Wood));
        Assert.False(chop.IsComplete(world, agent, ctx));
    }

    [Fact]
    public void ChopWoodDurationTest2()
    {
        // Arrange
        // See constructor

        // Act
        var ctx = chop.GetContext();
        for (int i = 0; i < 5; i++)
            chop.Execute(world, agent, ctx);

        // Assert
        Assert.Equal(5, ctx.Elapsed);
        Assert.Equal(4, forest.Wood);
        Assert.True(agent.Inventory[Keys.Wood] == 1);
        Assert.True(chop.IsComplete(world, agent, ctx));
    }

    [Fact]
    public void MoveToDurationTest1()
    {
        // Arrange
        // See constructor
        var distance = agent.Location.DistanceTo(forest.Location);

        // Act
        var ctx = chop.GetContext();
        move.Execute(world, agent, ctx);

        // Assert
        Assert.Equal(distance - 1, agent.Location.DistanceTo(forest.Location));
        Assert.False(move.IsComplete(world, agent, ctx));
    }

    [Fact]
    public void MoveToDurationTest2()
    {
        // Arrange
        // See constructor
        var distance = agent.Location.DistanceTo(forest.Location);

        // Act
        var ctx = chop.GetContext();
        for (int i = 0; i < distance; i++)
            move.Execute(world, agent, ctx);

        // Assert
        Assert.Equal(0, agent.Location.DistanceTo(forest.Location));
        Assert.True(move.IsComplete(world, agent, ctx));
    }
}
