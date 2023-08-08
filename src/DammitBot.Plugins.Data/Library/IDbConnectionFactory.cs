using System.Data;

namespace DammitBot.Library;

/// <summary>
/// Factory for creating and configuring instances of <see cref="IDbConnection"/>.
/// </summary>
public interface IDbConnectionFactory
{
    /// <summary>
    /// Configure, build, and return an <see cref="IDbConnection"/> for the given
    /// <paramref name="connectionString"/>.
    /// </summary>
    IDbConnection Build(string connectionString);
}