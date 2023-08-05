using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.CommandHandlers;

public interface ICommandHandlerService
    : IMessageHandlerService<ICommandHandler, CommandEventArgs> {}