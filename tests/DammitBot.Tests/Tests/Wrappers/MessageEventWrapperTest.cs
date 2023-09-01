using Bogus;
using DammitBot.Library;
using DammitBot.Protocols;
using DammitBot.Wrappers;
using SlackNet.Events;
using Xunit;

namespace DammitBot.Tests.Wrappers;

public class MessageEventWrapperTest : UnitTestBase<MessageEventWrapper>
{
    private MessageEvent? _messageEvent;
    
    protected override MessageEventWrapper CreateTarget()
    {
        return new MessageEventWrapper(
            _messageEvent = new Faker<MessageEvent>()
                .RuleFor(x => x.Text, f => f.Lorem.Text())
                .RuleFor(x => x.User, f => f.Person.UserName)
                .RuleFor(x => x.Channel, f => f.Commerce.Department())
                .Generate());
    }

    [Fact]
    public void Test_Message_ReturnsTextFromWrappedEvent()
    {
        Assert.Equal(_messageEvent!.Text, _target.Message);
    }

    [Fact]
    public void Test_Channel_ReturnsChannelFromWrappedEvent()
    {
        Assert.Equal(_messageEvent!.Channel, _target.Channel);
    }

    [Fact]
    public void Test_Protocol_ReturnsSlack()
    {
        Assert.Equal(nameof(Slack), _target.Protocol);
    }
    
    [Fact]
    public void Test_User_ReturnsUserFromWrappedEvent()
    {
        Assert.Equal(_messageEvent!.User, _target.User);
    }
}