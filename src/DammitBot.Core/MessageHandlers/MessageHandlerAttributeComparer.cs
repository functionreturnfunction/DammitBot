using System;
using DammitBot.Abstract;
using DammitBot.Metadata;

namespace DammitBot.MessageHandlers;

/// <summary>
/// Service which determines whether a given <see cref="Type"/> has a
/// <see cref="HandlesMessageAttribute"/> which matches a given message.
/// </summary>
public class MessageHandlerAttributeComparer
    : MessageHandlerAttributeComparerBase<HandlesMessageAttribute>, IMessageHandlerAttributeComparer {}