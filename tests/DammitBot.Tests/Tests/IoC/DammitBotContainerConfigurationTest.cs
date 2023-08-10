using DammitBot.IoC;
using DammitBot.Library;
using DammitBot.Utilities;
using Lamar;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace DammitBot.Tests.IoC;

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
    public void Test_Configure_SetsUpBotAsSingleton()
    {
        AssertSingleton<IBot, Bot>();
    }

    [Fact]
    public void Test_Configure_SetsUpAssemblyServiceAsSingleton()
    {
        AssertSingleton<IAssemblyService, AssemblyService>();
    }

    [Fact]
    public void Test_Configure_SetsUpConfigurationBuilder()
    {
        Assert.IsType<ConfigurationBuilder>(_target.GetInstance<IConfigurationBuilder>());
    }
}