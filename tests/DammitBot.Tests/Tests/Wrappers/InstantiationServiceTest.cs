using System;
using DammitBot.Library;
using DammitBot.Wrappers;
using Lamar;
using Moq;
using Xunit;

namespace DammitBot.Tests.Wrappers;

public class InstantiationServiceTest : UnitTestBase<InstantiationService>
{
    #region Private Members

    private Mock<IContainer>? _mockContainer;

    #endregion

    protected override IContainer CreateContainer()
    {
        _mockContainer = new Mock<IContainer>();
        return _mockContainer.Object;
    }

    protected override InstantiationService ConstructTarget()
    {
        return new InstantiationService(_mockContainer!.Object);
    }

    [Fact]
    public void Test_GenericGetInstance_GenericallyGetsInstanceFromContainer()
    {
        var now = DateTime.Now;
        _mockContainer!.Setup(x => x.GetInstance<DateTime>()).Returns(now);

        Assert.Equal(now, _target.GetInstance<DateTime>());
    }

    [Fact]
    public void Test_GetInstance_GetsInstanceFromContainer()
    {
        var now = DateTime.Now;
        _mockContainer!.Setup(x => x.GetInstance(typeof(DateTime))).Returns(now);

        Assert.Equal(now, _target.GetInstance(typeof(DateTime)));
    }
}