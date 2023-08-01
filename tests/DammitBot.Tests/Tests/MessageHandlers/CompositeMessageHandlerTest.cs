using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.MessageHandlers;

namespace DammitBot.Tests.MessageHandlers
{
    public class CompositeMessageHandlerTest
        : CompositeMessageHandlerTestBase<CompositeMessageHandler, IMessageHandler, MessageEventArgs> { }
}
