using System;
using System.Text.RegularExpressions;
using DammitBot.Configuration;
using DammitBot.Metadata;
using Microsoft.Extensions.Options;

namespace DammitBot.MessageHandlers;

/// <inheritdoc/>
/// <remarks>
/// This implementation looks for messages which are bot commands, meaning they begin with a name which
/// the the bot is configured to respond to.
/// </remarks>
public class CommandAwareMessageHandlerAttributeComparer : MessageHandlerAttributeComparer
{
    #region Private Members

    private readonly BotConfiguration _botConfig;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="CommandAwareMessageHandlerAttributeComparer"/> class.
    /// </summary>
    public CommandAwareMessageHandlerAttributeComparer(IOptions<BotConfiguration> botConfig)
    {
        _botConfig = botConfig.Value;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Returns a <see cref="Regex"/> which can parse a command message which has been sent to the bot.
    /// The regex should not match a message which does not being with a name the bot has been configured
    /// to go by/respond to.
    /// </summary>
    protected virtual Regex GetBotCommandRegex()
    {
        return new Regex($"^{_botConfig.GoesBy} .+");
    }

    #endregion

    #region Exposed Methods

    /// <inheritdoc />
    public override bool MessageMatches(string message, Type handlerType)
    {
        var attribute = GetAttribute(handlerType);

        return (attribute is HandlesBotMessageAttribute
            ? GetBotCommandRegex()
            : attribute.Regex)!.IsMatch(message);
    }

    #endregion
}