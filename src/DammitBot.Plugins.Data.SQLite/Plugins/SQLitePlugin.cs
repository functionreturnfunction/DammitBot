namespace DammitBot.Plugins;

/// <inheritdoc />
public class SQLitePlugin : ISQLitePlugin
{
    /// <inheritdoc />
    /// <remarks>
    /// This implementation sets up low-level access to SQLite.
    /// </remarks>
    public void Initialize()
    {
        SQLitePCL.Batteries_V2.Init();
    }

    /// <inheritdoc />
    /// <remarks>
    /// This implementation does nothing.
    /// </remarks>
    public void Cleanup() {}
}