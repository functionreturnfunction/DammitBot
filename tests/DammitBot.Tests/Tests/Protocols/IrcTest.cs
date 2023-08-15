using System;
using DammitBot.Configuration;
using DammitBot.Events;
using DammitBot.IoC;
using DammitBot.Library;
using DammitBot.Protocols;
using Lamar;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace DammitBot.Tests.Protocols;

public class IrcTest : UnitTestBase<Irc>
{
    private Mock<IIrcClientFactory>? _clientFactory;
    private Mock<IIrcClient>? _client;

    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);

        new IrcProtocolContainerConfiguration().Configure(serviceRegistry);

        _clientFactory = serviceRegistry.For<IIrcClientFactory>().Mock();

        _client = _clientFactory!
            .Setup(x => x.Build())
            .Mock();
    }

    [Fact]
    public void Test_Name_IsIrc()
    {
        Assert.Equal(nameof(Irc), _target.Name);
    }

    [Fact]
    public void Test_ChannelMessageReceivedEvent_IsPassedAlong()
    {
        var message =
            new MessageEventArgs("message", "channel", "protocol", "user");
        var bubbled = false;

        _target.ChannelMessageReceived += (_, args) => {
            bubbled = true;
            Assert.Equal(message, args);
        };
        _target.Initialize();
        
        _client!.Raise(x => x.ChannelMessageReceived += null, message);
        
        Assert.True(bubbled);
    }

    [Fact]
    public void Test_Initialize_BuildsClientAndConnects()
    {
        _target.Initialize();
        
        _clientFactory!.VerifyAll();
        _client!.Verify(x => x.Connect());
    }

    [Fact]
    public void Test_Cleanup_DisposesOfClient()
    {
        _target.Initialize();

        _target.Cleanup();
        
        _client!.Verify(x => x.Dispose());
    }

    [Fact]
    public void Test_Cleanup_ThrowsException_WhenNotInitialized()
    {
        Assert.Throws<InvalidOperationException>(() => _target.Cleanup());
    }

    [Fact]
    public void Test_ReadyToJoinChannelsEvent_CausesConfiguredChannelsToBeJoined()
    {
        var configuration = _container.GetInstance<IOptions<IrcConfiguration>>().Value;
        
        _target.Initialize();
        
        _client!.Raise(c => c.ReadyToJoinChannels += null, EventArgs.Empty);

        foreach (var channel in configuration.Channels)
        {
            _client.Verify(x => x.JoinChannel(channel));
        }
    }

    [Fact]
    public void Test_SayToChannel_SendsMessageToChannel()
    {
        var message = "blah blah blah";
        var channel = "#someChannel";
        
        _target.Initialize();
        
        _target.SayToChannel(channel, message);
        
        _client!.Verify(x => x.SendMessage(message, channel));
    }

    [Fact]
    public void Test_SayToChannel_SplitsMessagesContainingNewlines()
    {
        var message = string.Format("blah{0}blah{0}blah", Environment.NewLine);
        var channel = "#someChannel";
        
        _target.Initialize();
        
        _target.SayToChannel(channel, message);
        
        _client!.Verify(x => x.SendMessage("blah", channel), Times.Exactly(3));
    }

    [Fact]
    public void Test_SayToChannel_Throws_WhenNotInitialized()
    {
        Assert.Throws<InvalidOperationException>(
            () => _target.SayToChannel("foo", "bar"));
    }

    [Fact]
    public void Test_SayToAll_SendsMessageToAllConfiguredChannels()
    {
        var configuration = _container.GetInstance<IOptions<IrcConfiguration>>().Value;
        var message = "blah blah blah";
        
        _target.Initialize();
        _client!.Raise(x => x.ReadyToJoinChannels += null, EventArgs.Empty);
        
        _target.SayToAll(message);

        _client.Verify(x => x.SendMessage(message, configuration.Channels));
    }

    [Fact]
    public void Test_SayToAll_SplitsMessagesContainingNewlines()
    {
        var configuration = _container.GetInstance<IOptions<IrcConfiguration>>().Value;
        var message = string.Format("blah{0}blah{0}blah", Environment.NewLine);
        
        _target.Initialize();
        _client!.Raise(x => x.ReadyToJoinChannels += null, EventArgs.Empty);
        
        _target.SayToAll(message);

        _client.Verify(
            x => x.SendMessage("blah", configuration.Channels),
            Times.Exactly(3));
    }

    [Fact]
    public void Test_SayToAll_Throws_WhenNotInitialized()
    {
        Assert.Throws<InvalidOperationException>(
            () => _target.SayToAll("foo"));
    }
}