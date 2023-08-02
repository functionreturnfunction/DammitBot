using DammitBot.Events;

namespace DammitBot.Console;

public static class IBotExtensions
{
    public static void ReceiveConsoleMessage(this IBot bot, string message)
    {
        bot.ReceiveMessage(
            new MessageEventArgs(
            message,
            Protocols.Console.Console.PROTOCOL_NAME,
            Protocols.Console.Console.PROTOCOL_NAME,
            Protocols.Console.Console.PROTOCOL_NAME + " User"));
    }
}