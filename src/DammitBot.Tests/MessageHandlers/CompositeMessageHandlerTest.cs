using DammitBot.Abstract;
using DammitBot.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DammitBot.MessageHandlers
{
    [TestClass]
    public class CompositeMessageHandlerTest : CompositeMessageHandlerTestBase<CompositeMessageHandler, IMessageHandler, MessageEventArgs> { }
}
