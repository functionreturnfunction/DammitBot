using Lamar;

namespace DammitBot.Library;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    #region Private Members

    protected readonly IContainer _container;

    #endregion

    #region Constructors

    public UnitOfWorkFactory(IContainer container)
    {
        _container = container;
    }

    #endregion

    #region Exposed Methods

    public virtual IUnitOfWork Build() => _container.GetInstance<IUnitOfWork>();

    #endregion
}