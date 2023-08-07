namespace DammitBot.Library;

/// <summary>
/// Provider of connection strings for connecting to databases.
/// </summary>
public interface IConnectionStringProvider
{
    /// <summary>
    /// Returns the main connection string to be used by data access plugins.
    /// </summary>
    string GetMainAppConnectionString();
}