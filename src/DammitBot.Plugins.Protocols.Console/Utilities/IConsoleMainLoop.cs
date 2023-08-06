using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.Utilities;

public interface IConsoleMainLoop : IMainLoop
{
    event EventHandler<MessageEventArgs>? ChannelMessageReceived;
}