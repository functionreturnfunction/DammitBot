using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.CommandHandlers;

/// <summary>
/// Factory for creating instances of <see cref="ICommandHandler"/> which handle bot commands represented
/// by <see cref="CommandEventArgs"/>.
/// </summary>
public interface ICommandHandlerFactory : IMessageHandlerFactory<ICommandHandler, CommandEventArgs> {}