using System;
using System.Diagnostics.CodeAnalysis;
using ChatSharp;
using ChatSharp.Events;
using DammitBot.Events;
using DammitBot.Protocols.Irc.Events;
using DammitBot.Wrappers;

namespace DammitBot.Protocols.Irc.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class IrcClientWrapper : IIrcClient
    {
        #region Private Members

        private readonly IrcClient _innerClient;

        #endregion

        #region Constructors

        public IrcClientWrapper(IrcClient irc)
        {
            _innerClient = irc;
            _innerClient.ConnectionComplete += InnerClient_ConnectionComplete;
            _innerClient.ChannelMessageRecieved += InnerClient_ChannelMessageReceived;
        }

        #endregion

        #region Event Handlers

        private void InnerClient_ChannelMessageReceived(object sender, PrivateMessageEventArgs e)
        {
            ChannelMessageReceived?.Invoke(
                sender,
                new IrcMessageEventArgs(new PrivateMessageEventArgsWrapper(e)));
        }

        private void InnerClient_ConnectionComplete(object sender, EventArgs e)
        {
            ConnectionComplete?.Invoke(sender, e);
        }

        #endregion

        #region Events/Delegates

        public event EventHandler<MessageEventArgs> ChannelMessageReceived;

        public event EventHandler ConnectionComplete;

        #endregion

        #region Exposed Methods

        public void ConnectAsync()
        {
            _innerClient.ConnectAsync();
        }

        public void JoinChannel(string channel)
        {
            _innerClient.JoinChannel(channel);
        }

        public void SendMessage(string message, params string[] targets)
        {
            _innerClient.SendMessage(message, targets);
        }

        #endregion
    }
}
