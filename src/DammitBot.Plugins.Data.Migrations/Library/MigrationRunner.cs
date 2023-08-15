using System;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace DammitBot.Library;

/// <summary>
/// Runner of database schema migrations.  Provides operations to run individual or all migrations up or
/// down.
/// </summary>
public class MigrationRunner
{
    #region Constants
    
    private const string VERSION_INFO_TABLE = "VersionInfo";
    private const string CREATE_VERSION_INFO = @"
CREATE TABLE
IF NOT EXISTS " + VERSION_INFO_TABLE + @" (
    Id integer NOT NULL
);";
    
    #endregion
    
    #region Private Members

    private readonly IUnitOfWorkFactory _uowFactory;
    private readonly IMigrationService _service;
    private readonly ILogger<MigrationRunner> _logger;

    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="MigrationRunner"/> class.
    /// </summary>
    public MigrationRunner(
        IUnitOfWorkFactory uowFactory,
        IMigrationService service,
        ILogger<MigrationRunner> logger)
    {
        _uowFactory = uowFactory;
        _service = service;
        _logger = logger;
    }
    
    #endregion
    
    #region Private Methods

    private bool MigrationAlreadyRun(IUnitOfWork uow, MigrationBase migration)
    {
        return uow.ExecuteScalar(
            $"SELECT * FROM {VERSION_INFO_TABLE} WHERE Id = {migration.VersionNumber};") != null;
    }

    private void RunAll(
        Action<IUnitOfWork, MigrationBase> doRun,
        Action<IUnitOfWork, MigrationBase>? secondPass = null,
        bool reverse = false,
        int? upToVersionNumber = null)
    {
        var migrations = _service.Thingies.ToList();

        if (!migrations.Any())
        {
            return;
        }

        migrations = migrations.OrderBy(m => m.VersionNumber).ToList();

        if (upToVersionNumber.HasValue)
        {
            if (migrations.All(m => m.VersionNumber != upToVersionNumber.Value))
            {
                throw new ArgumentException(
                    $"Could not find migration with id {upToVersionNumber}.",
                    nameof(upToVersionNumber));
            }

            migrations = (reverse ?
                migrations.Where(m => m.VersionNumber > upToVersionNumber.Value) :
                migrations.Where(m => m.VersionNumber <= upToVersionNumber.Value)).ToList();
        }

        if (reverse)
        {
            migrations.Reverse();
        }

        using var uow = _uowFactory.Build();
        uow.ExecuteNonQuery(CREATE_VERSION_INFO);
        migrations = migrations.Where(m => MigrationAlreadyRun(uow, m) == reverse)
            .ToList();

        foreach (var migration in migrations)
        {
            _logger.LogInformation(
                "Running migration {VersionNumber} {Direction}",
                migration.VersionNumber,
                reverse ? "Down" : "Up");
            doRun(uow, migration);

            uow.ExecuteNonQuery(reverse
                ? $"DELETE FROM {VERSION_INFO_TABLE} WHERE Id = {migration.VersionNumber};"
                : $"INSERT INTO {VERSION_INFO_TABLE} (Id) VALUES ({migration.VersionNumber});");
        }

        if (secondPass != null)
        {
            foreach (var migration in migrations)
            {
                secondPass(uow, migration);
            }
        }

        uow.Commit();
    }
    
    #endregion
    
    #region Exposed Methods

    /// <summary>
    /// Run migration(s) up.  If <paramref name="versionNumber"/> is provided, only the migration with
    /// that version number will be run up (if it hasn't been already).  If <paramref name="seed"/> is set
    /// to false, seed data function(s) will be skipped. 
    /// </summary>
    public void Up(int? versionNumber = null, bool seed = true)
    {
        if (seed)
        {
            RunAll(
                (u, m) => m.Up(u),
                (u, m) => m.Seed(u),
                upToVersionNumber: versionNumber);
        }
        else
        {
            RunAll((u, m) => m.Up(u), upToVersionNumber: versionNumber);
        }
    }

    /// <summary>
    /// Run migration(s) down.  If <paramref name="versionNumber"/> is provided, only the migration with
    /// that version number will be run down (if it has previously been applied).
    /// </summary>
    public void Down(int? versionNumber = null)
    {
        RunAll(
            (u, m) => m.Down(u),
            reverse: true,
            upToVersionNumber: versionNumber);
    }

    /// <summary>
    /// Retrieve the highest version number of any known migration which has been run against the
    /// configured database.   
    /// </summary>
    public int? GetLatestVersionNumber()
    {
        using var uow = _uowFactory.Build();
        uow.ExecuteNonQuery(CREATE_VERSION_INFO);
        var num = uow.ExecuteScalar($"SELECT MAX(Id) FROM {VERSION_INFO_TABLE};");
        return num is DBNull ? (int?)null : Convert.ToInt32(num);
    }
    
    #endregion
}