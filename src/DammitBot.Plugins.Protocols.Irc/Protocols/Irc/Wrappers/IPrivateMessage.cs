namespace DammitBot.Protocols.Irc.Wrappers;

public interface IPrivateMessage
{
    string Message { get; }
    string Nick { get; }
}