using DammitBot.IoC;
using DammitBot.Library;
using DammitBot.MessageHandlers;
using Lamar;
using Xunit;

namespace DammitBot.Tests.IoC;

public class CommandsPluginContainerConfigurationTest : InMemoryDatabaseUnitTestBase<IContainer>
{
    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);

        new CommandsPluginContainerConfiguration().Configure(serviceRegistry);
    }

    [Fact]
    public void Test_Configure_SetsUpMessageHandlerAttributeComparer()
    {
        Assert.IsType<CommandAwareMessageHandlerAttributeComparer>(
            _target.GetInstance<IMessageHandlerAttributeComparer>());
    }
}