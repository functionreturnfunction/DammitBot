using DammitBot.Events;
using DammitBot.IoC;
using DammitBot.Library;
using DammitBot.Protocols;
using DammitBot.Utilities;
using Lamar;
using Moq;
using Xunit;

namespace DammitBot.Tests.Protocols;

public class ConsoleTest : UnitTestBase<Console>
{
    private Mock<IConsoleMainLoop>? _mainLoop;

    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);
        
        new ConsoleProtocolContainerConfiguration().Configure(serviceRegistry); 

        _mainLoop = serviceRegistry.For<IConsoleMainLoop>().Mock();
    }

    [Fact]
    public void Test_Name_IsConsole()
    {
        Assert.Equal(nameof(Console), _target.Name);
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
        
        _mainLoop!.Raise(x => x.ChannelMessageReceived += null, message);
        
        Assert.True(bubbled);
    }

    [Fact]
    public void Test_SayToAll_WritesFormattedMessageToMainLoop()
    {
        var message = "blah blah blah";
        
        _target.SayToAll(message);
        
        _mainLoop!.Verify(x => x.WriteLine($"###TO ALL: {message}"));
    }

    [Fact]
    public void Test_SayToChannel_WritesFormattedMessageToMainLoop()
    {
        var message = "blah blah blah";
        var channel = "#someChannel";
        
        _target.SayToChannel(channel, message);
        
        _mainLoop!.Verify(x => x.WriteLine($"###TO '{channel}': {message}"));
    }
}