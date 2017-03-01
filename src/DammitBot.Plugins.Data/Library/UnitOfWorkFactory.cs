using StructureMap;

namespace DammitBot.Data.Library
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        #region Private Members

        private readonly IContainer _container;

        #endregion

        #region Constructors

        public UnitOfWorkFactory(IContainer container)
        {
            _container = container;
        }

        #endregion

        #region Exposed Methods

        public IUnitOfWork Build() => _container.GetInstance<IUnitOfWork>();

        #endregion
    }
}