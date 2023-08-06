using System;
using DammitBot.Abstract;
using Microsoft.Extensions.Logging;

namespace DammitBot.Utilities;

/// <inheritdoc cref="IMainLoop"/>
/// <remarks>
/// This implementation provides two overridable methods; <see cref="DoPreLoopStuff"/>, which is called
/// once before the main loop, and <see cref="DoLoopStuff"/>.  This should be overridden by inheritors in
/// order to perform any necessary tasks prior to the loop running, and then over and over again until the
/// bot exits, respectively. 
/// </remarks>
public class MainLoop : IMainLoop
{
    /// <summary>
    /// <see cref="IBot"/> instance, useful for checking on its status and such.
    /// </summary>
    protected IBot Bot { get; }
    private readonly ILogger<MainLoop> _log;

    /// <summary>
    /// Constructor for the <see cref="MainLoop"/> class.
    /// </summary>
    // ReSharper disable once MemberCanBeProtected.Global
    public MainLoop(IBot bot, ILogger<MainLoop> log)
    {
        Bot = bot;
        _log = log;
    }

    /// <summary>
    /// Action(s) to perform once before the main loop runs.
    /// </summary>
    protected virtual void DoPreLoopStuff() {}

    /// <summary>
    /// Action(s) to perform repeatedly inside the main application loop.
    /// </summary>
    protected virtual void DoLoopStuff() {}

    /// <inheritdoc cref="IMainLoop.Run"/>
    public void Run()
    {
        Bot.Run();

        try
        {
            DoPreLoopStuff();
            
            do
            {
                DoLoopStuff();
            } while (Bot.Running);
        }
        catch (Exception e)
        {
            _log.LogError(e, "Runtime error occurred");
            throw;
        }
    }

    /// <inheritdoc cref="IDisposable.Dispose"/>
    /// <remarks>
    /// This implementation disposes the <see cref="IBot"/> instance.
    /// </remarks>
    public void Dispose()
    {
        Bot.Dispose();
    }
}