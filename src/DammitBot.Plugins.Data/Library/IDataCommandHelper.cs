using System.Linq;

namespace DammitBot.Data.Library
{
    public interface IDataCommandHelper
    {
        #region Abstract Methods

        object Insert(object entity);
        void Update(object entity);
        T Load<T>(int id) where T : class;
        IQueryable<T> GetQueryable<T>() where T : class;

        #endregion
    }
}
