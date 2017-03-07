using System;
using System.Collections.Generic;
using System.Linq;
using FluentMigrator;
using FluentMigrator.Infrastructure;
using FluentMigrator.Infrastructure.Extensions;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Processors.SqlServer;
using log4net;

namespace DammitBot.Utilities
{
    public class MigrationService : IMigrationService
    {
        #region Private Members

        private readonly IAssemblyService _assemblyService;
        private readonly IMigrationRunnerFactory _runnerFactory;
        private readonly IFlushableLogAnnouncer _flushableLogAnnouncer;
        private IAssemblyCollection _migrationAssemblies;
        private IEnumerable<Type> _migrationTypes;
        private long? _latestVersionNumber;

        #endregion

        #region Properties

        public long? LatestVersionNumber => _latestVersionNumber ?? (_latestVersionNumber = GetLatestVersionNumber());

        #endregion

        #region Constructors

        public MigrationService(IAssemblyService assemblyService, IMigrationRunnerFactory runnerFactory, IFlushableLogAnnouncer flushableLogAnnouncer)
        {
            _assemblyService = assemblyService;
            _runnerFactory = runnerFactory;
            _flushableLogAnnouncer = flushableLogAnnouncer;
        }

        #endregion

        #region Private Methods

        private long? GetLatestVersionNumber()
        {
            long? toReturn = null;
            // Look through all types
            foreach (var t in GetMigrationTypes())
            {
                // Get all the types with MigrationAttribute (object[] because it can have multiple Migration attributes)
                var attributes = t.GetCustomAttributes(typeof(MigrationAttribute), true);
                    // Get the max of (current max, max version specified in this Type's Migration attributes)
                toReturn = Math.Max(toReturn ?? 0, attributes.Max(o => (o as MigrationAttribute).Version));
            }

            return toReturn;
        }

        private IEnumerable<Type> GetMigrationTypes()
        {
            return _migrationTypes ??
                   (_migrationTypes =
                       GetMigrationAssemblies().Assemblies.SelectMany(a => a.GetTypes().Where(TypeIsMigration)));
        }

        private IMigrationRunner GetMigrator(IMigrationProcessorOptions options)
        {
            return _runnerFactory.Build(GetMigrationAssemblies(),
                options ?? new MigrationProcessorOptions<IFlushableLogAnnouncer, SqlServer2008ProcessorFactory>());
        }

        private IAssemblyCollection GetMigrationAssemblies()
        {
            return _migrationAssemblies ?? (_migrationAssemblies =
                new AssemblyCollection(
                    _assemblyService.GetAllAssemblies()
                        .Where(a => a.GetTypes().Any(TypeIsMigration))));
        }

        private static bool TypeIsMigration(Type type)
        {
            return !type.IsAbstract && typeof(Migration).IsAssignableFrom(type) &&
                   type.HasAttribute<MigrationAttribute>();
        }

        private void Run(IMigrationProcessorOptions options, Action<IMigrationRunner> fn)
        {
            fn(GetMigrator(options));
            _flushableLogAnnouncer.Flush();
        }

        #endregion

        #region Exposed Methods

        public void EnsureUpToDate(IMigrationProcessorOptions options = null)
        {
            if (LatestVersionNumber.HasValue)
            {
                Run(options, runner => runner.MigrateUp(LatestVersionNumber.Value));
            }
            else
            {
                throw new InvalidOperationException("No migrations found!");
            }
        }

        #endregion
    }
}
