using System.Diagnostics.CodeAnalysis;
using DammitBot.Library;

namespace DammitBot.Plugins;

/// <inheritdoc />
public class AutoMigrationsPlugin : IAutoMigrationsPlugin
{
    #region Private Members
    
    private readonly MigrationRunner _runner;
    
    #endregion
    
    #region Properties

    /// <inheritdoc />
    public bool Priority => true;

    #endregion
    
    #region Constructors
    
    /// <summary>
    /// Constructor for the <see cref="AutoMigrationsPlugin"/> class.
    /// </summary>
    /// <param name="runner"></param>
    public AutoMigrationsPlugin(MigrationRunner runner)
    {
        _runner = runner;
    }
    
    #endregion
    
    #region Exposed Methods

    /// <inheritdoc />
    /// <remarks>This implementation runs any pending database migrations.</remarks>
    public void Initialize()
    {
        _runner.Up();
    }

    /// <inheritdoc />
    /// <remarks>This implementation does nothing.</remarks>
    [ExcludeFromCodeCoverage]
    public void Cleanup() {}
    
    #endregion
}