using DammitBot.MessageHandlers;

namespace DammitBot.Metadata;

/// <summary>
/// Special implementation of <see cref="HandlesMessageAttribute"/> which denotes interception of a
/// message which was sent to the bot, i.e. a command.  This is handled differently in
/// <see cref="CommandAwareMessageHandlerAttributeComparer"/>, and should only ever be used for this
/// specific purpose.
/// </summary>
internal class HandlesBotMessageAttribute : HandlesMessageAttribute
{
    /// <summary>
    /// Constructor for the <see cref="HandlesBotMessageAttribute"/> class.
    /// </summary>
    public HandlesBotMessageAttribute() : base(string.Empty) {}
}