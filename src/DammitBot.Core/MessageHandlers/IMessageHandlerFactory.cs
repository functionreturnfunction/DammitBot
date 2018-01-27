using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.MessageHandlers
{
    public interface IMessageHandlerFactory : IHandlerFactory<IMessageHandler, MessageEventArgs> {}
}