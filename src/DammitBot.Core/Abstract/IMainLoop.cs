using System;

namespace DammitBot.Abstract;

/// <summary>
/// Main program loop, which will initialize the bot and then run a blocking loop.  Meant to be used as
/// the main loop in an application thread, such as in Main() in a console application.
/// </summary>
public interface IMainLoop : IDisposable
{
    /// <summary>
    /// Run the loop.
    /// </summary>
    void Run();
}