using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.CommandHandlers
{
    public interface ICommandHandlerRepository
        : IMessageHandlerRepository<ICommandHandler, CommandEventArgs> {}
}