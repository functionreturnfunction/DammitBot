using DammitBot.Ioc;
using DammitBot.Library;
using DammitBot.MessageHandlers;
using Lamar;
using Xunit;

namespace DammitBot.Tests.Ioc;

public class CommandsPluginContainerConfigurationTest : InMemoryDatabaseUnitTestBase<IContainer>
{
    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);

        new CommandsPluginContainerConfiguration().Configure(serviceRegistry);
    }

    [Fact]
    public void TestConfigureSetsUpMessageHandlerAttributeComparer()
    {
        Assert.IsType<CommandAwareMessageHandlerAttributeComparer>(
            _target.GetInstance<IMessageHandlerAttributeComparer>());
    }
}