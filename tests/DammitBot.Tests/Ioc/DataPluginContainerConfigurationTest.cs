using DammitBot.Data.Library;
using DammitBot.Ioc;
using DammitBot.TestLibrary;
using StructureMap;
using Xunit;

namespace DammitBot.Tests.Ioc
{
    public class DataPluginContainerConfigurationTest : InMemoryDatabaseUnitTestBase<IContainer>
    {
        public DataPluginContainerConfigurationTest()
        {
            _container.Configure(e =>
                new DataPluginContainerConfiguration().Configure(e));
        }

        [Fact]
        public void TestConfigureSetsUpUnitOfWorkFactory()
        {
            Assert.IsType<UnitOfWorkFactory>(_target.GetInstance<IUnitOfWorkFactory>());
        }
    }
}