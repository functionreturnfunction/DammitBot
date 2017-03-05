using System.Linq;

namespace DammitBot.Data.Library
{
    public class PersistenceService : IPersistenceService
    {
        #region Private Members

        private readonly IUnitOfWork _unitOfWork;

        #endregion

        #region Constructors

        public PersistenceService(IUnitOfWorkFactory factory)
        {
            _unitOfWork = factory.Build();
        }

        #endregion

        #region Exposed Methods

        public void Dispose()
        {
            _unitOfWork.Commit();
            _unitOfWork.Dispose();
        }

        public T Save<T>(T obj)
        {
            return _unitOfWork.GetRepository<T>().Save(obj);
        }

        public T Find<T>(object id)
        {
            return _unitOfWork.GetRepository<T>().Find(id);
        }

        public IQueryable<T> Query<T>()
        {
            return _unitOfWork.GetRepository<T>();
        }

        #endregion
    }
}
