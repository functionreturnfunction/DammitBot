﻿using System;

namespace DammitBot.Events;

public class MessageEventArgs
{
    public MessageEventArgs(string message, string channel, string protocol, string user)
    {
        Message = message;
        Channel = channel;
        Protocol = protocol;
        User = user;
    }

    #region Properties

    public virtual string Message { get; }
    public virtual string Channel { get; }
    public virtual string Protocol { get; }
    public virtual string User { get; }

    #endregion
}