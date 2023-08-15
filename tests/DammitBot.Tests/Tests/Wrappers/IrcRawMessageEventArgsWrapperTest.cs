using DammitBot.Library;
using DammitBot.Protocols;
using DammitBot.Wrappers;
using IrcDotNet;
using Moq;
using Xunit;

namespace DammitBot.Tests.Wrappers;

public class IrcRawMessageEventArgsWrapperTest : UnitTestBase<IrcRawMessageEventArgsWrapper>
{
    private const string CHANNEL = "#channel";
    private const string MESSAGE = "blah blah blah";
    private const string NICK = "somebody";

    protected override IrcRawMessageEventArgsWrapper ConstructTarget()
    {
        var source = new Mock<IIrcMessageSource>();
        source.Setup(x => x.Name).Returns(NICK);
        return new IrcRawMessageEventArgsWrapper(new IrcRawMessageEventArgs(
            new IrcClient.IrcMessage {
                Parameters = new[] { CHANNEL, MESSAGE },
                Source = source.Object 
            },
            null));
    }

    [Fact]
    public void Test_Protocol_IsSetAppropriately()
    {
        Assert.Equal(nameof(Irc), _target.Protocol);
    }

    [Fact]
    public void Test_Channel_IsSetAppropriately()
    {
        Assert.Equal(CHANNEL, _target.Channel);
    }

    [Fact]
    public void Test_Message_IsSetAppropriately()
    {
        Assert.Equal(MESSAGE, _target.Message);
    }

    [Fact]
    public void Test_User_IsSetAppropriately()
    {
        Assert.Equal(NICK, _target.User);
    }
}