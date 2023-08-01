using System.Linq;

namespace DammitBot.Library
{
    public interface IDataCommandHelper
    {
        #region Abstract Methods

        object Insert<TEntity>(TEntity entity) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        T Load<T>(int id) where T : class;
        IQueryable<T> GetQueryable<T>() where T : class;

        #endregion
    }
}
