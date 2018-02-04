using DammitBot.Ioc;
using DammitBot.MessageHandlers;
using DammitBot.TestLibrary;
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