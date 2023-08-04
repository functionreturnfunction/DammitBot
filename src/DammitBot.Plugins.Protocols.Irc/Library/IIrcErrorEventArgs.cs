using System;

namespace DammitBot.Library;

public interface IIrcErrorEventArgs
{
    Exception Exception { get; }
}