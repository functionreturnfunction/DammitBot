using DammitBot.Ioc;
using DammitBot.Library;
using DammitBot.Utilities;
using Microsoft.Extensions.Configuration;
using StructureMap;
using Xunit;

namespace DammitBot.Tests.Ioc;

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

    [Fact]
    public void TestConfigureSetsUpLoggerRepositoryAsSingleton()
    {
        // AssertSingleton<ILoggerRepository, ILoggerRepository>();
        Assert.True(false, "Tests not yet written.");
    }

    [Fact]
    public void TestConfigureSetsUpConfigurationBuilder()
    {
        Assert.IsType<ConfigurationBuilder>(_target.GetInstance<IConfigurationBuilder>());
    }
}