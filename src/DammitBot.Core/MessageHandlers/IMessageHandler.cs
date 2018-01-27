using DammitBot.Abstract;
using DammitBot.Events;

namespace DammitBot.MessageHandlers
{
    public interface IMessageHandler : IMessageHandler<MessageEventArgs> {}

}