using System.Text.RegularExpressions;
using DammitBot.Configuration;
using DammitBot.Events;

namespace DammitBot.Utilities;

/// <summary>
/// Extensions to the <see cref="MessageEventArgs"/> class.
/// </summary>
public static class MessageEventArgsExtensions
{
    /// <summary>
    /// Get the part of the text of a message which can be considered to be a command, i.e. the message
    /// text with the configured name the bot goes by/responds to removed from the beginning.
    /// </summary>
    public static string GetCommandText(this MessageEventArgs args, BotConfiguration config)
    {
        return Regex.Match(args.Message, config.GoesBy + " (.+)").Groups[1].Value;
    }
}