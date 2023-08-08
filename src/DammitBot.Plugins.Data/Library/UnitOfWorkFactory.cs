using DammitBot.Wrappers;

namespace DammitBot.Library;

/// <inheritdoc />
public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    #region Private Members

    private readonly IInstantiationService _instantiationService;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="UnitOfWorkFactory"/> class.
    /// </summary>
    public UnitOfWorkFactory(IInstantiationService instantiationService)
    {
        _instantiationService = instantiationService;
    }

    #endregion

    #region Exposed Methods

    /// <inheritdoc />
    public virtual IUnitOfWork Build() => _instantiationService.GetInstance<IUnitOfWork>();

    #endregion
}