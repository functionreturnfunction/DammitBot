using DammitBot.IoC;
using DammitBot.Library;
using DammitBot.Utilities;
using Lamar;
using Lamar.Microsoft.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace DammitBot.Tests.IoC;

public class DammitBotContainerConfigurationTest : UnitTestBase<IContainer>
{
    protected override IContainer CreateContainer()
    {
        var host = Host
            .CreateDefaultBuilder()
            .UseLamar(ConfigureContainer)
            .Build();

        return host.Services.GetRequiredService<IContainer>();
    }

    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        new DammitBotContainerConfiguration().Configure(serviceRegistry);

        base.ConfigureContainer(serviceRegistry);
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
        AssertSingleton<IAssemblyTypeService, AssemblyTypeService>();
    }

    [Fact]
    public void Test_Configure_SetsUpConfigurationBuilder()
    {
        Assert.IsType<ConfigurationBuilder>(_target.GetInstance<IConfigurationBuilder>());
    }
}