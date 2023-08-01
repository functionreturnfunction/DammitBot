using DammitBot.Ioc;
using DammitBot.Library;
using DammitBot.MessageHandlers;
using StructureMap;
using Xunit;

namespace DammitBot.Tests.Ioc
{
    public class CommandsPluginContainerConfigurationTest : InMemoryDatabaseUnitTestBase<IContainer>
    {
        public CommandsPluginContainerConfigurationTest()
        {
            _container.Configure(e => new CommandsPluginContainerConfiguration().Configure(e));
        }

        [Fact]
        public void TestConfigureSetsUpMessageHandlerAttributeService()
        {
            Assert.IsType<CommandAwareMessageHandlerAttributeService>(_target.GetInstance<IMessageHandlerAttributeService>());
        }
    }
}