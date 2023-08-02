using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.CommandHandlers;

public interface ICommandHandler : IMessageHandler<CommandEventArgs> {}