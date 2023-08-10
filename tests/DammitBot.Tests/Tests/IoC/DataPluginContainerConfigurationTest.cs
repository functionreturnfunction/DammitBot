using DammitBot.IoC;
using DammitBot.Library;
using Lamar;
using Xunit;

namespace DammitBot.Tests.IoC;

public class DataPluginContainerConfigurationTest : InMemoryDatabaseUnitTestBase<IContainer>
{
    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);
        new DataPluginContainerConfiguration().Configure(serviceRegistry);
    }

    [Fact]
    public void Test_Configure_SetsUpUnitOfWorkFactory()
    {
        Assert.IsType<UnitOfWorkFactory>(_target.GetInstance<IUnitOfWorkFactory>());
    }
}