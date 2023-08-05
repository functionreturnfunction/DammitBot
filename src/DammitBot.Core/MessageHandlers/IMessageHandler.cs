using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.MessageHandlers;

/// <summary>
/// Handler of messages represented by <see cref="MessageEventArgs"/>.
/// </summary>
public interface IMessageHandler : IMessageHandler<MessageEventArgs> {}