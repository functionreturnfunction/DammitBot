using System.Diagnostics.CodeAnalysis;
using FluentMigrator;
using FluentMigrator.Infrastructure;
using FluentMigrator.Runner.Initialization;

namespace DammitBot.Utilities
{
    [ExcludeFromCodeCoverage]
    public class MigrationRunner : FluentMigrator.Runner.MigrationRunner
    {
        public MigrationRunner(IAssemblyCollection assemblies, IRunnerContext runnerContext, IMigrationProcessor processor) : base(assemblies, runnerContext, processor) {}
    }
}