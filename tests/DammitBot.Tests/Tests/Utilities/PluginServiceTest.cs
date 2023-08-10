using System;
using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.Library;
using DammitBot.Utilities;
using Lamar;
using Xunit;

namespace DammitBot.Tests.Utilities;

public class PluginServiceTest : UnitTestBase<PluginService>
{
    private TestPlugin? _testPlugin;

    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);

        serviceRegistry.For<ITestPlugin>().Use(_testPlugin = new TestPlugin());

        serviceRegistry.For<IAssemblyService>().Use(new TestAssemblyService(false));
    }

    [Fact]
    public void TestInitializingInitializesProtocols()
    {
        _target.Initialize();
        
        Assert.True(_testPlugin!.IsInitialized);
    }
    
    #region Nested Classes

    public class TestPlugin : ITestPlugin
    {
        public bool IsInitialized { get; private set; }
        
        public void Initialize()
        {
            IsInitialized = true;
        }

        public void Cleanup() {}

        public string Name { get; }
        public void SayToAll(string message) {}

        public void SayToChannel(string channel, string message) {}

        public event EventHandler<MessageEventArgs>? ChannelMessageReceived;
    }
    
    public interface ITestPlugin : IPlugin {}
    
    #endregion
}