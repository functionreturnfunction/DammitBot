using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.MessageHandlers;

/// <summary>
/// Factory for creating instances of <see cref="IMessageHandler"/> which handle messages represented by
/// instances of <see cref="MessageEventArgs"/>. 
/// </summary>
public interface IMessageHandlerFactory : IMessageHandlerFactory<IMessageHandler, MessageEventArgs> {}