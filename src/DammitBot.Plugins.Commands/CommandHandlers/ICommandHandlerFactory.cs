using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.CommandHandlers;

public interface ICommandHandlerFactory : IMessageHandlerFactory<ICommandHandler, CommandEventArgs> {}