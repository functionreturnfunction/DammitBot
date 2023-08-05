using System;
using DammitBot.Library;
using IrcDotNet;

namespace DammitBot.Wrappers;

public class IrcErrorEventArgsWrapper : IIrcErrorEventArgs
{
    private readonly IrcErrorEventArgs _innerEventArgs;

    public Exception Exception => _innerEventArgs.Error;

    public IrcErrorEventArgsWrapper(IrcErrorEventArgs innerEventArgs)
    {
        _innerEventArgs = innerEventArgs;
    }
}