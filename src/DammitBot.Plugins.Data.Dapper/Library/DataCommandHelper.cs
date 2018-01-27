using System;
using System.Linq;
using DammitBot.Data.Library;

namespace DammitBot.Data.Dapper.Library
{
    public class DataCommandHelper : IDataCommandHelper
    {
        #region Exposed Methods

        public void Save(object entity)
        {
            throw new NotImplementedException();
        }

        public T Load<T>(object id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetQueryable<T>()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
