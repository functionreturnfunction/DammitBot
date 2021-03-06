﻿using System.Text.RegularExpressions;
using DammitBot.Events;
using DammitBot.Protocols.Irc.Wrappers;

namespace DammitBot.Protocols.Irc.Events
{
    public class IrcMessageEventArgs : MessageEventArgs
    {
        #region Constructors

        public IrcMessageEventArgs(PrivateMessageEventArgsWrapper args) : base(args.PrivateMessage.Message, ParseChannel(args), Irc.PROTOCOL_NAME, args.PrivateMessage.Nick) {}

        private static string ParseChannel(PrivateMessageEventArgsWrapper args)
        {
            return Regex.Match(args.IrcMessage.RawMessage, @"PRIVMSG ([^\s]+) :").Groups[1].Value;
        }

        #endregion
    }
}
