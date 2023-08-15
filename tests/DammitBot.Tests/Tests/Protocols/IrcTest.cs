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

public class IrcTest : UnitTestBase<Irc>
{
    private Mock<IIrcClientFactory>? _clientFactory;
    private Mock<IIrcClient>? _client;

    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);

        _clientFactory = serviceRegistry.For<IIrcClientFactory>().Mock();

        _client = _clientFactory!
            .Setup(x => x.Build())
            .Mock();
        
        new IrcProtocolContainerConfiguration().Configure(serviceRegistry);
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
}