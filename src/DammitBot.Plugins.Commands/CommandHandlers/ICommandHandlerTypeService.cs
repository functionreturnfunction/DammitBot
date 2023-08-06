using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.CommandHandlers;

/// <summary>
/// Service which provides inheriting/implementing types of <see cref="ICommandHandler"/> which
/// can handle messages represented by <see cref="CommandEventArgs"/>.
/// </summary>
public interface ICommandHandlerTypeService
    : IMessageHandlerTypeService<ICommandHandler, CommandEventArgs> {}