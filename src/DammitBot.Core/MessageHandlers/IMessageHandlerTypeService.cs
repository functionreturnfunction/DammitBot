using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.MessageHandlers;

/// <summary>
/// Service which provides inheriting/implementing types of <see cref="IMessageHandler"/> which
/// can handle messages represented by <see cref="MessageEventArgs"/>.
/// </summary>
public interface IMessageHandlerTypeService
    : IMessageHandlerTypeService<IMessageHandler, MessageEventArgs> {}