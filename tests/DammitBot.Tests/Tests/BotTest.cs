using System;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.Library;
using DammitBot.MessageHandlers;
using DammitBot.Utilities;
using Lamar;
using Moq;
using Xunit;

namespace DammitBot.Tests;

public class BotTest : UnitTestBase<Bot>
{
    #region Private Members

    private Mock<IBotConfigurationSection> _config;
    private Mock<IMessageHandlerFactory> _handlerFactory;
    private Mock<IPluginService> _pluginService;
    private Mock<IProtocolService> _protocolService;

    #endregion

    #region Private Methods

    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);
        _config = new Mock<IBotConfigurationSection>();
        var manager = serviceRegistry.For<IConfigurationProvider>().Mock();
        manager.SetupGet(x => x.BotConfig).Returns(_config.Object);
        _handlerFactory = serviceRegistry.For<IMessageHandlerFactory>().Mock();
        _pluginService = serviceRegistry.For<IPluginService>().Mock();
        _protocolService = serviceRegistry.For<IProtocolService>().Mock();
    }

    private void SafelyRunTarget()
    {
        // die ensures the Loop doesn't run
        _target.Die();
        _target.Run();
    }

    #endregion

    #region Exposed Methods

    [Fact]
    public void Test_Run_ThrowsException_IfAlreadyRunning()
    {
        SafelyRunTarget();

        Assert.Throws<InvalidOperationException>(() => _target.Run());
    }

    [Fact]
    public void Test_ChannelMessageReceived_HandlesMessageWithHandlerFromHandlerFactory()
    {
        var args = new MessageEventArgs(
            "foo",
            string.Empty,
            string.Empty,
            string.Empty);
        _handlerFactory.Setup(
            f => f.BuildHandler(args).Handle(args));
        SafelyRunTarget();
        _protocolService.Raise(
            c => c.ChannelMessageReceived += null,
            null,
            args);

        _handlerFactory.Verify(
            f => f.BuildHandler(args).Handle(args));
    }

    [Fact]
    public void Test_SayToAll_SaysToAll()
    {
        SafelyRunTarget();
        _target.SayToAll("foo");

        _protocolService.Verify(c => c.SayToAll("foo"));
    }

    [Fact]
    public void Test_ReplyToMessage_RepliesToMessage()
    {
        var args = new MessageEventArgs(
            string.Empty,
            "bar",
            "foo",
            string.Empty);
        SafelyRunTarget();

        _target.ReplyToMessage(args, "baz");

        _protocolService.Verify(
            x => x.SayToChannel(
                "foo",
                "bar",
                "baz"));
    }

    [Fact]
    public void Test_DisposingBot_CleansUpPluginService()
    {
        SafelyRunTarget();

        _target.Dispose();

        _pluginService.Verify(x => x.Cleanup());
    }

    #endregion
}