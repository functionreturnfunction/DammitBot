using System;
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

        _console = serviceRegistry.For<IConsole>().Mock();
        _irc = serviceRegistry.For<IIrc>().Mock();
        serviceRegistry.For<IInstantiationService>().Use<InstantiationService>();
        serviceRegistry.For<IAssemblyService>().Use<AssemblyService>();
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
    public void TestCleanupCleansUpAllProtocols()
    {
        _target.Initialize();
        _target.Cleanup();

        _console!.Verify(x => x.Cleanup());
    }

    [Fact]
    public void TestSayToAllSaysToAllProtocols()
    {
        _target.Initialize();
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

    #endregion
}