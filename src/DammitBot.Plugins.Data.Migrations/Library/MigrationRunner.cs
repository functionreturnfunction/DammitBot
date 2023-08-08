using System;
using System.Linq;

namespace DammitBot.Library;

public class MigrationRunner
{
    public const string VERSION_INFO_TABLE = "VersionInfo";
    public const string CREATE_VERSION_INFO = @"
CREATE TABLE
IF NOT EXISTS " + VERSION_INFO_TABLE + @" (
    Id integer NOT NULL
);";

    protected readonly IUnitOfWorkFactory _uowFactory;
    protected readonly IMigrationService _service;

    public MigrationRunner(IUnitOfWorkFactory uowFactory, IMigrationService service)
    {
        _uowFactory = uowFactory;
        _service = service;
    }

    protected bool MigrationAlreadyRun(IUnitOfWork uow, MigrationBase migration)
    {
        return uow.ExecuteScalar(
            $"SELECT * FROM {VERSION_INFO_TABLE} WHERE Id = {migration.VersionNumber};") != null;
    }

    protected void RunAll(
        Action<IUnitOfWork, MigrationBase> doRun,
        Action<IUnitOfWork, MigrationBase>? secondPass = null,
        bool reverse = false,
        int? upToId = null)
    {
        var migrations = _service.Thingies.ToList();

        if (!migrations.Any())
        {
            return;
        }

        migrations = migrations.OrderBy(m => m.VersionNumber).ToList();

        if (upToId.HasValue)
        {
            if (!migrations.Any(m => m.VersionNumber == upToId.Value))
            {
                throw new ArgumentException(
                    $"Could not find migration with id {upToId}.",
                    nameof(upToId));
            }

            migrations = (reverse ?
                migrations.Where(m => m.VersionNumber > upToId.Value) :
                migrations.Where(m => m.VersionNumber <= upToId.Value)).ToList();
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

    public void Up(int? id = null, bool seed = true)
    {
        if (seed)
        {
            RunAll(
                (u, m) => m.Up(u),
                (u, m) => m.Seed(u),
                upToId: id);
        }
        else
        {
            RunAll((u, m) => m.Up(u), upToId: id);
        }
    }

    public void Down(int? id = null)
    {
        RunAll((u, m) => m.Down(u), reverse: true, upToId: id);
    }

    public int? GetLatestVersionNumber()
    {
        using var uow = _uowFactory.Build();
        uow.ExecuteNonQuery(CREATE_VERSION_INFO);
        var num = uow.ExecuteScalar($"SELECT MAX(Id) FROM {VERSION_INFO_TABLE};");
        return num is DBNull ? (int?)null : Convert.ToInt32(num);
    }
}