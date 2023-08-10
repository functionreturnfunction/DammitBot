using System;
using System.Linq;
using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.Library;
using DammitBot.Protocols.Console;
using DammitBot.Protocols.Irc;
using DammitBot.Utilities;
using DammitBot.Wrappers;
using Lamar;
using Moq;
using Xunit;

namespace DammitBot.Tests.Utilities;

public class ProtocolServiceTest : UnitTestBase<ProtocolService>
{
    #region Private Members

    private Mock<IConsole>? _console;
    private Mock<IIrc>? _irc;
    private TestProtocol _testProtocol;
    private readonly MessageEventArgs _args;

    public ProtocolServiceTest()
    {
        _args = new MessageEventArgs("message", "channel", "protocol", "user");
    }

    #endregion

    #region Private Methods

    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);

        serviceRegistry.For<ITestProtocol>().Use(_testProtocol = new TestProtocol());

        _console = serviceRegistry.For<IConsole>().Mock();
        _irc = serviceRegistry.For<IIrc>().Mock();
        serviceRegistry.For<IInstantiationService>().Use<InstantiationService>();
        serviceRegistry.For<IAssemblyService>().Use<TestAssemblyService>();
    }

    #endregion

    #region Exposed Methods

    [Fact]
    public void TestAttachingToChannelMessageReceivedAttachesToEachProtocol()
    {
        bool called = false;
        EventHandler<MessageEventArgs> handler = (s, a) => {
            called = true;
            Assert.Same(_args, a);
        };

        _target.Initialize();
        _target.ChannelMessageReceived += handler;
        _console!.Raise(x => x.ChannelMessageReceived += null, null, _args);

        Assert.True(called);
    }

    [Fact]
    public void TestDetachingFromChannelMessageReceivedDetachesFromEachProtocol()
    {
        bool called = false;
        EventHandler<MessageEventArgs> handler = (s, a) => {
            called = true;
        };

        _target.Initialize();
        _target.ChannelMessageReceived += handler;
        _target.ChannelMessageReceived -= handler;
        _console!.Raise(x => x.ChannelMessageReceived += null, null, _args);

        Assert.False(called);
    }

    [Fact]
    public void TestCleanupCleansUpAllProtocolsWhenTheyAreInitialized()
    {
        // ensure things get initialized
        var _ = _target.Thingies.ToList();
        _target.Cleanup();

        _console!.Verify(x => x.Cleanup());
    }

    [Fact]
    public void TestCleanupDoesNotCleanUpAllProtocolsWhenTheyHaveNotBeenInitialized()
    {
        _target.Cleanup();

        _console!.Verify(x => x.Cleanup(), Times.Never);
    }

    [Fact]
    public void TestSayToAllSaysToAllProtocols()
    {
        _target.SayToAll("foo");

        _console!.Verify(x => x.SayToAll("foo"));
    }

    [Fact]
    public void TestSayToChannelSaysToSpecificChannelOfSpecificProtocol()
    {
        _console!.SetupGet(x => x.Name)
            .Returns(Protocols.Console.Console.PROTOCOL_NAME);
        _target.Initialize();

        _target.SayToChannel(Protocols.Console.Console.PROTOCOL_NAME, "foo", "bar");

        _console.Verify(x => x.SayToChannel("foo", "bar"));
    }

    [Fact]
    public void TestInitializingInitializesProtocols()
    {
        _target.Initialize();
        
        Assert.True(_testProtocol.IsInitialized);
    }

    #endregion
    
    #region Nested Classes

    public class TestProtocol : ITestProtocol
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
    
    public interface ITestProtocol : IProtocol {}
    
    #endregion
}