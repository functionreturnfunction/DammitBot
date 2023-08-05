using System;
using System.Text.RegularExpressions;
using DammitBot.Configuration;
using DammitBot.Metadata;

namespace DammitBot.MessageHandlers;

public class CommandAwareMessageHandlerAttributeComparer : MessageHandlerAttributeComparer
{
    #region Private Members

    private readonly IBotConfigurationSection _botConfig;

    #endregion

    #region Constructors

    public CommandAwareMessageHandlerAttributeComparer(IConfigurationProvider configurationProvider)
    {
        _botConfig = configurationProvider.BotConfig;
    }

    #endregion

    #region Private Methods

    protected virtual Regex GetBotMessageRegex()
    {
        return new Regex($"^{_botConfig.GoesBy} .+");
    }

    #endregion

    #region Exposed Methods

    public override bool MessageMatches(string message, Type handlerType)
    {
        var attribute = GetAttribute(handlerType);

        return (attribute is HandlesBotMessageAttribute
            ? GetBotMessageRegex()
            : attribute.Regex)!.IsMatch(message);
    }

    #endregion
}