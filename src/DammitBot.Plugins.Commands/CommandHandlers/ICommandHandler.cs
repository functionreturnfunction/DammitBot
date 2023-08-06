using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.CommandHandlers;

/// <summary>
/// Handler of <see cref="CommandEventArgs"/> events, i.e. bot commands.
/// </summary>
public interface ICommandHandler : IMessageHandler<CommandEventArgs> {}