using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DammitBot.Events;
using DammitBot.Protocols.Irc.Wrappers;

namespace DammitBot.Protocols.Irc.Events
{
    public class IrcMessageEventArgs : MessageEventArgs
    {
        public IrcMessageEventArgs(PrivateMessageEventArgsWrapper args) : base(args.PrivateMessage.Message, null, Irc.PROTOCOL_NAME, args.PrivateMessage.Nick) {}
    }
}
