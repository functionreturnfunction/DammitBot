using DammitBot.Abstract;
using DammitBot.IoC;
using DammitBot.Library;
using DammitBot.Protocols;
using DammitBot.Utilities;
using Lamar;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace DammitBot.Tests.Utilities;

public class ConsoleMainLoopTest : UnitTestBase<IConsoleMainLoop>
{
    private Mock<IBot>? _bot;
    private Mock<IConsoleIO>? _console;
    
    protected override void ConfigureContainer(ServiceRegistry serviceRegistry)
    {
        base.ConfigureContainer(serviceRegistry);

        new ConsoleProtocolContainerConfiguration().Configure(serviceRegistry);

        serviceRegistry.For(typeof(ILogger<>)).Use(typeof(MockLogger<>));

        _bot = serviceRegistry.For<IBot>().Mock();
        _console = serviceRegistry.For<IConsoleIO>().Mock();
    }

    [Fact]
    public void Test_WriteLine_WritesValueToConsole()
    {
        var value = "blah blah blah";
        
        _target.WriteLine(value);
        
        _console!.Verify(x => x.WriteLine(value));
    }

    [Fact]
    public void Test_Run_RunsBot()
    {
        _target.Run();
        
        _bot!.Verify(x => x.Run());
    }

    [Fact]
    public void Test_Run_PromptsForUserInput()
    {
        _target.Run();
        
        _console!.Verify(x => x.Write(ConsoleMainLoop.USER_PROMPT), Times.Once);
    }

    [Fact]
    public void Test_Run_FiresChannelMessageReceivedEvent_WhenUserEntersInput()
    {
        var userInput = "blah blah blah";
        var eventFired = false;

        _console!.Setup(x => x.KeyAvailable).Returns(true);
        _console.Setup(x => x.ReadLine()).Returns(userInput);

        _target.ChannelMessageReceived += (_, args) => {
            eventFired = true;
            Assert.Equal(userInput, args.Message);
            Assert.Equal(nameof(Console), args.Protocol);
            Assert.Equal(nameof(Console), args.Channel);
            Assert.Equal(nameof(Console) + " User", args.User);
        };

        _target.Run();
        
        Assert.True(eventFired);
    }

    [Fact]
    public void Test_Run_PromptsForUserInputAgain_AfterReceivingInput()
    {
        _console!.Setup(x => x.KeyAvailable).Returns(true);
        _console.Setup(x => x.ReadLine()).Returns("blah blah blah");
        _bot!.SetupSequence(x => x.Running).Returns(true).Returns(false);

        _target.Run();
        
        _console!.Verify(
            x => x.Write(ConsoleMainLoop.USER_PROMPT),
            Times.Exactly(2));
    }

    [Fact]
    public void Test_Target_IsTheMainLoop()
    {
        Assert.Same(_target, _container.GetInstance<IMainLoop>());
    }
}