using System;

namespace DammitBot.Abstract;

/// <summary>
/// Interface used for plugin (and plugin-like thing) initialization (and cleanup).  Implement this in
/// your plugin (or plugin-like thing) assembly if you have tasks which need completing only once
/// before/after everything else.
/// </summary>
/// <remarks>
/// This interface is not meant to be implemented directly; use a more specific/purposeful interface such
/// as <see cref="IPlugin"/> or <see cref="IProtocol"/> instead.
/// </remarks>
public interface IPluginThingy
{
    /// <summary>
    /// If set to true, the plugin will be initialized immediately rather than waiting for all other
    /// plugins to be gathered and instantiated first.  This is useful for things like migration
    /// runners which need to create state (set things up) which will be utilized by other plugins.
    /// </summary>
    bool Priority => false;

    /// <summary>
    /// Perform any initialization tasks such as creating directories/files or connecting to external
    /// resources.
    /// </summary>
    void Initialize();
    
    /// <summary>
    /// Perform any cleanup tasks such as disconnecting from external resources or disposing of
    /// <see cref="IDisposable"/>s.
    /// </summary>
    void Cleanup();
}