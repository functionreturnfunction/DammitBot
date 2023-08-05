using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.MessageHandlers;
using Xunit;

namespace DammitBot.Tests.MessageHandlers;

public class MessageHandlerFactoryTest
    : MessageHandlerFactoryTestBase<
        MessageHandlerFactory,
        IMessageHandlerTypeService,
        IMessageHandler,
        MessageEventArgs>
{
    protected override MessageEventArgs CreateEventArgs() => CreateMessageEventArgs();

    [Fact]
    public override void TestHandleCallsHandleOnEachInnerHandler()
    {
        base.TestHandleCallsHandleOnEachInnerHandler();
    }
}