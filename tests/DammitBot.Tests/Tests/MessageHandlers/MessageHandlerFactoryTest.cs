using DammitBot.Abstract;
using DammitBot.Events;
using DammitBot.MessageHandlers;
using Xunit;

namespace DammitBot.Tests.MessageHandlers
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