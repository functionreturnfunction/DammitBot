using System;
using System.Data;
using System.Linq;
using DammitBot.Data.Library;
using Dommel;

namespace DammitBot.Data.Dapper.Library
{
    public class DataCommandHelper : IDataCommandHelper
    {
        #region Private Members

        private readonly IDbConnection _connection;

        #endregion

        #region Constructors

        public DataCommandHelper(IDbConnection connection)
        {
            _connection = connection;
        }

        #endregion

        #region Exposed Methods

        public object Insert(object entity)
        {
            return _connection.Insert(entity);
        }

        public void Update(object entity)
        {
            _connection.Update(entity);
        }

        public T Load<T>(int id)
            where T : class
        {
            return _connection.Get<T>(id);
        }

        public IQueryable<T> GetQueryable<T>()
            where T : class
        {
            return _connection.GetAll<T>().ToList().AsQueryable();
        }

        #endregion
    }
}
