using DammitBot.Data.Models;
using DammitBot.Data.Models.Fakers;
using DammitBot.Library;
using Lamar;
using Moq;
using Xunit;

namespace DammitBot.Tests.Library;

public class RepositoryTest : UnitTestBase<Repository<Nick>>
{
    private Mock<IDataCommandService> _dataCommandHelper;

    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);
        _dataCommandHelper = serviceRegistry.For<IDataCommandService>().Mock();
    }

    [Fact]
    public void Test_Insert_SavesAndReturnsIdentifier()
    {
        var entity = new NickFaker().Generate();

        _dataCommandHelper.Setup(x => x.Insert(entity)).Returns(666);
            
        Assert.Equal(666, _target.Insert(entity));

        _dataCommandHelper.Verify(x => x.Insert(entity));
    }

    [Fact]
    public void Test_Find_LoadsEntity()
    {
        var entity = new NickFaker().Generate();
        _dataCommandHelper.Setup(x => x.Load<Nick>(666))
            .Returns(entity);

        Assert.Same(entity, _target.Find(666));
    }
}