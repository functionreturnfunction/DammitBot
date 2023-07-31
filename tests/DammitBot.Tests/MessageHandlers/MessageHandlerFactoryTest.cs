using DammitBot.Abstract;
using DammitBot.Events;
using Xunit;

namespace DammitBot.MessageHandlers
{
    public class MessageHandlerFactoryTest : MessageHandlerFactoryTestBase<MessageHandlerFactory, IMessageHandlerRepository, IMessageHandler, MessageEventArgs>
    {
        [Fact]
        public override void TestHandleCallsHandleOnEachInnerHandler()
        {
            base.TestHandleCallsHandleOnEachInnerHandler();
        }
    }
}