using DammitBot.Ioc;
using DammitBot.Library;
using Lamar;
using Xunit;

namespace DammitBot.Tests.Ioc;

public class DataPluginContainerConfigurationTest : InMemoryDatabaseUnitTestBase<IContainer>
{
    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);
        new DataPluginContainerConfiguration().Configure(serviceRegistry);
    }

    [Fact]
    public void TestConfigureSetsUpUnitOfWorkFactory()
    {
        Assert.IsType<UnitOfWorkFactory>(_target.GetInstance<IUnitOfWorkFactory>());
    }
}