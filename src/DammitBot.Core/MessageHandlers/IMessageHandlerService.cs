using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.MessageHandlers;

public interface IMessageHandlerService
    : IMessageHandlerService<IMessageHandler, MessageEventArgs> {}