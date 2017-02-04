using System;
using System.Linq;
using DammitBot.Abstract;
using DammitBot.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DammitBot.MessageHandlers
{
    [TestClass]
    public class MessageHandlerFactoryTest : MessageHandlerFactoryTestBase<MessageHandlerFactory, IMessageHandlerRepository, IMessageHandler, MessageEventArgs>
    {
    }
}