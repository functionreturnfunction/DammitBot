using System;
using DammitBot.Configuration;
using DammitBot.IoC;
using DammitBot.Library;
using DammitBot.Protocols;
using Lamar;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace DammitBot.Tests.Protocols;

public class IrcTest : ProtocolTestBase<Irc, IIrcClient, IIrcClientFactory>
{
    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);

        new IrcProtocolContainerConfiguration().Configure(serviceRegistry);
    }

    [Fact]
    public void Test_Cleanup_DisposesOfClient()
    {
        _target.Initialize();

        _target.Cleanup();
        
        _client!.Verify(x => x.Dispose());
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
    public void Test_SayToChannel_SplitsMessagesContainingNewlines()
    {
        var message = string.Format("blah{0}blah{0}blah", Environment.NewLine);
        var channel = "#someChannel";
        
        _target.Initialize();
        
        _target.SayToChannel(channel, message);
        
        _client!.Verify(x => x.SendMessage("blah", channel), Times.Exactly(3));
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
}