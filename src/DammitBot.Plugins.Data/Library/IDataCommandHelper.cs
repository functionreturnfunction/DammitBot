using System.Linq;

namespace DammitBot.Data.Library
{
    public interface IDataCommandHelper
    {
        #region Abstract Methods

        void Save(object entity);
        T Load<T>(object id);
        IQueryable<T> GetQueryable<T>();

        #endregion
    }
}