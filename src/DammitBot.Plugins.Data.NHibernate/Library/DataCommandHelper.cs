using System.Linq;
using DammitBot.Data.Library;
using NHibernate;
using NHibernate.Linq;

namespace DammitBot.Data.NHibernate.Library
{
    public class DataCommandHelper : IDataCommandHelper
    {
        #region Private Members

        private readonly ISession _session;

        #endregion

        #region Constructors

        public DataCommandHelper(ISession session)
        {
            _session = session;
        }

        #endregion

        #region Exposed Methods

        public void Save(object entity)
        {
            _session.SaveOrUpdate(entity);
        }

        public T Load<T>(object id)
        {
            return _session.Load<T>(id);
        }

        public IQueryable<T> GetQueryable<T>()
        {
            return _session.Query<T>();
        }

        #endregion
    }
}