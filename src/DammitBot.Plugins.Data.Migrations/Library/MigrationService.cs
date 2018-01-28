using DammitBot.Abstract;
using DammitBot.Utilities;
using DammitBot.Wrappers;

namespace DammitBot.Data.Migrations.Library
{
    public class MigrationService : AssemblyServiceThingyBase<MigrationBase>
    {
        public MigrationService(IAssemblyService assemblyService, IInstantiationService instantiationService) : base(assemblyService, instantiationService) {}
    }
}