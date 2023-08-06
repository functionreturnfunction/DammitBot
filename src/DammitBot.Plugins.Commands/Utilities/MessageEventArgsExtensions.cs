using System.Text.RegularExpressions;
using DammitBot.Configuration;
using DammitBot.Events;

namespace DammitBot.Utilities;

/// <summary>
/// Extensions to the <see cref="MessageEventArgs"/> class.
/// </summary>
public static class MessageEventArgsExtensions
{
    public static string GetCommandText(this MessageEventArgs args, IBotConfigurationSection config)
    {
        return Regex.Match(args.Message, config.GoesBy + " (.+)").Groups[1].Value;
    }
}