using DammitBot.Abstract;
using DammitBot.Metadata;

namespace DammitBot.MessageHandlers;

/// <inheritdoc cref="IMessageHandlerAttributeComparer{HandlesMessageAttribute}" />
/// <remarks>
/// This implementation looks for and compares <see cref="HandlesMessageAttribute"/>s. 
/// </remarks>
public interface IMessageHandlerAttributeComparer
    : IMessageHandlerAttributeComparer<HandlesMessageAttribute> {}