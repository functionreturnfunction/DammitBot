using System;
using DammitBot.Events;

namespace DammitBot.Abstract
{
    public interface IProtocol : IPluginThingy
    {
        #region Abstract Properties

        string Name { get; }

        #endregion

        #region Abstract Methods

        void SayToAll(string message);
        void SayToChannel(string channel, string message);

        #endregion

        #region Events/Delegates

        event EventHandler<MessageEventArgs> ChannelMessageReceived;

        #endregion
    }
}