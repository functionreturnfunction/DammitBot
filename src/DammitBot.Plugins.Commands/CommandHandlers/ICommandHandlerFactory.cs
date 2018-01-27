using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.CommandHandlers
{
    public interface ICommandHandlerFactory : IHandlerFactory<ICommandHandler, CommandEventArgs> {}
}