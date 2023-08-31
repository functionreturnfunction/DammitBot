using System;
using DammitBot.Abstract;
using DammitBot.Events;
using Lamar;
using Moq;
using Xunit;

namespace DammitBot.Library;

public abstract class ProtocolTestBase<TProtocol, TClient, TClientFactory> : UnitTestBase<TProtocol>
    where TProtocol : IProtocol
    where TClient : class, IProtocolClient
    where TClientFactory : class, IProtocolClientFactory<TClient>
{
    private Mock<TClientFactory>? _clientFactory;
    protected readonly Mock<TClient> _client;

    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);

        _clientFactory = serviceRegistry.For<TClientFactory>().Mock();
    }

    public ProtocolTestBase()
    {
        _client = _clientFactory!.Setup(x => x.Build()).Mock();
    }

    [Fact]
    public void Test_Name_IsNameOfProtocol()
    {
        Assert.Equal(typeof(TProtocol).Name, _target.Name);
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
        
        _client!.Raise(x => x.MessageReceived += null, message);
        
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
    public void Test_Cleanup_ThrowsException_WhenNotInitialized()
    {
        Assert.Throws<InvalidOperationException>(() => _target.Cleanup());
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
    public void Test_SayToChannel_Throws_WhenNotInitialized()
    {
        Assert.Throws<InvalidOperationException>(
            () => _target.SayToChannel("foo", "bar"));
    }

    [Fact]
    public void Test_SayToAll_Throws_WhenNotInitialized()
    {
        Assert.Throws<InvalidOperationException>(
            () => _target.SayToAll("foo"));
    }
}