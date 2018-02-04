using DammitBot.TestLibrary;
using DammitBot.Utilities;
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

        private void AssertSingleton<TIType, TType>()
            where TType : TIType
        {
            var thing = _target.GetInstance<TIType>();
            Assert.IsType<TType>(thing);
            Assert.Same(thing, _target.GetInstance<TIType>());
        }

        [Fact]
        public void TestConfigureSetsUpBotAsSingleton()
        {
            AssertSingleton<IBot, Bot>();
        }

        [Fact]
        public void TestConfigureSetsUpAssemblyServiceAsSingleton()
        {
            AssertSingleton<IAssemblyService, AssemblyService>();
        }
    }
}