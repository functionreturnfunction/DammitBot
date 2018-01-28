using System;

namespace DammitBot.Data.Library
{
    public interface IDisposableUnitOfWork : IUnitOfWork, IDisposable
    {
        #region Abstract Methods

        void Commit();
        IRepository<T> GetRepository<T>() where T : class;
        int ExecuteNonQuery(string sql);
        object ExecuteScalar(string sql);

        #endregion
    }
}
