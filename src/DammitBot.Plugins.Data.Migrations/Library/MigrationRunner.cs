using System;
using System.Collections.Generic;
using System.Linq;
using DammitBot.Data.Library;

namespace DammitBot.Data.Migrations.Library
{
    public class MigrationRunner
    {
        public const string VERSION_INFO_TABLE = "VersionInfo";
        public const string CREATE_VERSION_INFO = @"
CREATE TABLE
IF NOT EXISTS " + VERSION_INFO_TABLE + @" (
    Id integer NOT NULL
);";

        protected readonly IUnitOfWorkFactory _uowFactory;
        protected readonly MigrationService _service;

        public MigrationRunner(IUnitOfWorkFactory uowFactory, MigrationService service)
        {
            _uowFactory = uowFactory;
            _service = service;
        }

        protected bool MigrationAlreadyRun(IDisposableUnitOfWork uow, MigrationBase migration)
        {
            return uow.ExecuteScalar($"SELECT * FROM {VERSION_INFO_TABLE} WHERE Id = {migration.Id};") != null;
        }

        protected void RunAll(Action<IDisposableUnitOfWork, MigrationBase> doRun, Action<IDisposableUnitOfWork, MigrationBase> secondPass = null, bool reverse = false)
        {
            var migrations = _service.Thingies.OrderBy(m => m.Id).ToList();
            if (reverse)
            {
                migrations.Reverse();
            }

            using (var uow = _uowFactory.Build().Start())
            {
                uow.ExecuteNonQuery(CREATE_VERSION_INFO);
                migrations = migrations.Where(m => MigrationAlreadyRun(uow, m) == reverse).ToList();

                foreach (var migration in migrations)
                {
                    doRun(uow, migration);

                    uow.ExecuteNonQuery(reverse
                        ? $"DELETE FROM {VERSION_INFO_TABLE} WHERE Id = {migration.Id};"
                        : $"INSERT INTO {VERSION_INFO_TABLE} (Id) VALUES ({migration.Id});");
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
        }

        public void Up(bool seed = true)
        {
            if (seed)
            {
                RunAll((u, m) => m.Up(u), (u, m) => m.Seed(u));
            }
            else
            {
                RunAll((u, m) => m.Up(u));
            }
        }

        public void Down()
        {
            RunAll((u, m) => m.Down(u), reverse: true);
        }
    }
}