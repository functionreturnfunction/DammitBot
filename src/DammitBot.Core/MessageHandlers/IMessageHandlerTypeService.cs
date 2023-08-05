using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.MessageHandlers;

public interface IMessageHandlerTypeService
    : IMessageHandlerTypeService<IMessageHandler, MessageEventArgs> {}