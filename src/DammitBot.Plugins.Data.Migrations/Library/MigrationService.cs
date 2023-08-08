using DammitBot.Abstract;
using DammitBot.Utilities;
using DammitBot.Wrappers;

namespace DammitBot.Library;

/// <inheritdoc cref="IMigrationService"/>
public class MigrationService : ThingyServiceBase<MigrationBase>, IMigrationService
{
    #region Constructors
    
    /// <summary>
    /// Constructor for the <see cref="MigrationService"/> class.
    /// </summary>
    public MigrationService(
        IAssemblyService assemblyService,
        IInstantiationService instantiationService)
        : base(assemblyService, instantiationService) {}
    
    #endregion
}