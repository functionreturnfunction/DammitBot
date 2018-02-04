using DammitBot.TestLibrary;
using StructureMap;
using Xunit;

namespace DammitBot.Ioc
{
    public class DammitBotContainerConfigurationTest : InMemoryDatabaseUnitTestBase<IContainer>
    {
        public DammitBotContainerConfigurationTest()
        {
            _container.Configure(e =>
                new DammitBotContainerConfiguration().Configure(e));
        }

        [Fact]
        public void TestConfigureSetsUpBotAsSingleton()
        {
            var bot = _target.GetInstance<IBot>();
            Assert.IsType<Bot>(bot);
            Assert.Same(bot, _target.GetInstance<IBot>());
        }
    }
}