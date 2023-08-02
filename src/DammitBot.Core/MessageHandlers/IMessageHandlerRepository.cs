using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.MessageHandlers;

public interface IMessageHandlerRepository
    : IMessageHandlerRepository<IMessageHandler, MessageEventArgs> {}