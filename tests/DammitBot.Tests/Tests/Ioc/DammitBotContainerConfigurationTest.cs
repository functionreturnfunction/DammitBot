using DammitBot.Ioc;
using DammitBot.Library;
using DammitBot.Utilities;
using Lamar;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace DammitBot.Tests.Ioc;

public class DammitBotContainerConfigurationTest : InMemoryDatabaseUnitTestBase<IContainer>
{
    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);
        
        new DammitBotContainerConfiguration().Configure(serviceRegistry);
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