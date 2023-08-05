using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.CommandHandlers;

public interface ICommandHandlerTypeService
    : IMessageHandlerTypeService<ICommandHandler, CommandEventArgs> {}