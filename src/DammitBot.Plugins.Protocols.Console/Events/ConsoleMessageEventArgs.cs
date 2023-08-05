namespace DammitBot.Events;

public class ConsoleMessageEventArgs : MessageEventArgs
{
    public ConsoleMessageEventArgs(string message, string channel, string protocol, string user)
        : base(message, channel, protocol, user) { }
}