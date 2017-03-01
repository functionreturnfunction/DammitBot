using System;

namespace DammitBot.Data.Library
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        IRepository<T> GetRepository<T>();
    }
}