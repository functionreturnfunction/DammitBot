using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DammitBot.Configuration;
using FluentMigrator;
using FluentMigrator.Infrastructure;
using FluentMigrator.Infrastructure.Extensions;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors.SqlServer;
using log4net;

namespace DammitBot.Utilities
{
    public class MigrationService : IMigrationService
    {
        #region Private Members

        private readonly IAssemblyService _assemblyService;
        private readonly IDataConfigurationManager _dataConfiguration;
        private readonly ILog _log;
        private AssemblyCollection _migrationAssemblies;
        private IEnumerable<Type> _migrationTypes;

        #endregion

        #region Properties

        public long LatestVersionNumber
        {
            get
            {
                long toReturn = 0;
                // Look through all types
                foreach (var t in GetMigrationTypes())
                {
                    // Get all the types with MigrationAttribute (object[] because it can have multiple Migration attributes)
                    object[] attributes = t.GetCustomAttributes(typeof(MigrationAttribute), true);
                    if (attributes.Length > 0)
                    {
                        // Get the max of (current max, max version specified in this Type's Migration attributes)
                        toReturn = Math.Max(toReturn, attributes.Max(o => (o as MigrationAttribute).Version));
                    }
                }

                return toReturn;
            }
        }

        #endregion

        #region Constructors

        public MigrationService(ILog log, IDataConfigurationManager dataConfiguration, IAssemblyService assemblyService)
        {
            _log = log;
            _dataConfiguration = dataConfiguration;
            _assemblyService = assemblyService;
        }

        #endregion

        #region Private Methods

        private IEnumerable<Type> GetMigrationTypes()
        {
            return _migrationTypes ??
                   (_migrationTypes =
                       GetMigrationAssemblies().Assemblies.SelectMany(a => a.GetTypes().Where(TypeIsMigration)));
        }

        private MigrationRunner GetMigrator()
        {
            var announcer = new TextWriterAnnouncer(s => _log.Info(s));

            var migrationContext = new RunnerContext(announcer);
            var factory = new SqlServer2008ProcessorFactory();
            var processor = factory.Create(_dataConfiguration.ConnectionString, announcer,
                new Options {PreviewOnly = false, Timeout = 60});
            var runner = new MigrationRunner(GetMigrationAssemblies(), migrationContext, processor);

            return runner;
        }

        private AssemblyCollection GetMigrationAssemblies()
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

        #endregion

        #region Exposed Methods

        public void EnsureUpToDate()
        {
            GetMigrator().MigrateUp(LatestVersionNumber);
        }

        #endregion

        #region Nested Type: AssemblyCollection

        public class AssemblyCollection : IAssemblyCollection
        {
            #region Properties

            public Assembly[] Assemblies { get; }

            #endregion

            #region Constructors

            public AssemblyCollection(IEnumerable<Assembly> assemblies)
            {
                Assemblies = assemblies.ToArray();
            }

            #endregion

            #region Exposed Methods

            public Type[] GetExportedTypes()
            {
                return Assemblies.SelectMany(a => a.GetExportedTypes()).ToArray();
            }

            public ManifestResourceNameWithAssembly[] GetManifestResourceNames()
            {
                return Assemblies.SelectMany(
                    a => a.GetManifestResourceNames().Select(
                        n => new ManifestResourceNameWithAssembly(n, a)))
                        .ToArray();
            }

            #endregion
        }

        #endregion

        #region Nested Type: Options

        public class Options : IMigrationProcessorOptions
        {
            #region Properties

            public bool PreviewOnly { get; set; }
            public int Timeout { get; set; }
            public string ProviderSwitches { get; set; }

            #endregion
        }

        #endregion
    }
}
