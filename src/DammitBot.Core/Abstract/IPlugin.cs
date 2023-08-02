namespace DammitBot.Abstract;

/// <summary>
/// Interface used for plugin initialization (and cleanup).  Implement this in your plugin if you have
/// tasks which need completing only once before/after everything else.
/// </summary>
public interface IPlugin : IPluginThingy
{
}