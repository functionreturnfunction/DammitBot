using DammitBot.Abstract;

namespace DammitBot.Plugins;

/// <summary>
/// <see cref="IPlugin"/> which will run any pending database migrations against the configured database
/// on bot startup.  This is marked as a <see cref="IPlugin.Priority"/> so as to run early on during
/// startup before any other plugins which might require the migrations to be current.
/// </summary>
public interface IAutoMigrationsPlugin : IPlugin {}