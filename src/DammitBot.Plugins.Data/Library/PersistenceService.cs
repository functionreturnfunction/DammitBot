using System.Linq;

namespace DammitBot.Data.Library
{
    public class PersistenceService : IPersistenceService
    {
        #region Private Members

        private readonly IUnitOfWorkFactory _factory;
        private IDisposableUnitOfWork _unitOfWork;

        #endregion

        #region Properties

        public virtual IUnitOfWork UnitOfWork => DisposableUnitOfWork;
        protected virtual IDisposableUnitOfWork DisposableUnitOfWork => _unitOfWork ?? (_unitOfWork = _factory.Build().Start());

        #endregion

        #region Constructors

        public PersistenceService(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

        #endregion

        #region Exposed Methods

        public void Dispose()
        {
            if (_unitOfWork == null)
            {
                return;
            }

            _unitOfWork.Commit();
            _unitOfWork.Dispose();
        }

        public object Insert<T>(T obj)
            where T : class
        {
            return DisposableUnitOfWork.GetRepository<T>().Insert(obj);
        }

        public void Update<T>(T obj)
            where T : class
        {
            DisposableUnitOfWork.GetRepository<T>().Update(obj);
        }

        public T Find<T>(int id)
            where T : class
        {
            return DisposableUnitOfWork.GetRepository<T>().Find(id);
        }

        public IQueryable<T> Query<T>()
            where T : class
        {
            return DisposableUnitOfWork.GetRepository<T>();
        }

        #endregion
    }
}
