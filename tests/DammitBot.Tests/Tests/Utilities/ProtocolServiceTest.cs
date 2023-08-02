﻿using System;
using DammitBot.Events;
using DammitBot.Library;
using DammitBot.Utilities;
using DammitBot.Wrappers;
using Moq;
using Xunit;

namespace DammitBot.Tests.Utilities;

public class ProtocolServiceTest : UnitTestBase<ProtocolService>
{
    #region Private Members

    private Mock<Protocols.Console.Console>? _console;

    #endregion

    #region Private Methods

    protected override void ConfigureContainer()
    {
        base.ConfigureContainer();
        Inject(out _console);
        Inject<IInstantiationService>(_container.GetInstance<InstantiationService>());
        Inject<IAssemblyService>(_container.GetInstance<AssemblyService>());
    }

    #endregion

    #region Exposed Methods

    [Fact]
    public void TestAttachingToChannelMessageReceivedAttachesToEachProtocol()
    {
        bool called = false;
        var args = new MessageEventArgs();
        EventHandler<MessageEventArgs> handler = (s, a) => {
            called = true;
            Assert.Same(args, a);
        };

        _target.Initialize();
        _target.ChannelMessageReceived += handler;
        _console!.Raise(x => x.ChannelMessageReceived += null, null, args);

        Assert.True(called);
    }

    [Fact]
    public void TestDetachingFromChannelMessageReceivedDetachesFromEachProtocol()
    {
        bool called = false;
        var args = new MessageEventArgs();
        EventHandler<MessageEventArgs> handler = (s, a) => {
            called = true;
        };

        _target.Initialize();
        _target.ChannelMessageReceived += handler;
        _target.ChannelMessageReceived -= handler;
        _console!.Raise(x => x.ChannelMessageReceived += null, null, args);

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