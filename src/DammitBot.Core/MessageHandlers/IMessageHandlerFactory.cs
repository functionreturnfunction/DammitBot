using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.MessageHandlers;

public interface IMessageHandlerFactory : IMessageHandlerFactory<IMessageHandler, MessageEventArgs> {}