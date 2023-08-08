using DammitBot.Library;

namespace DammitBot.Plugins;

/// <inheritdoc />
public class AutoMigrationsPlugin : IAutoMigrationsPlugin
{
    private readonly MigrationRunner _runner;

    /// <inheritdoc />
    public bool Priority => true;

    /// <summary>
    /// Constructor for the <see cref="AutoMigrationsPlugin"/> class.
    /// </summary>
    /// <param name="runner"></param>
    public AutoMigrationsPlugin(MigrationRunner runner)
    {
        _runner = runner;
    }

    /// <inheritdoc />
    /// <remarks>This implementation runs any pending database migrations.</remarks>
    public void Initialize()
    {
        _runner.Up();
    }

    /// <inheritdoc />
    /// <remarks>This implementation does nothing.</remarks>
    public void Cleanup() {}
}