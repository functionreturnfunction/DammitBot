using System;
using Microsoft.Extensions.DependencyInjection;

namespace DammitBot.Wrappers
{
    public class InstantiationService : IInstantiationService
    {
        #region Private Members

        private readonly IServiceCollection _container;

        #endregion

        #region Constructors

        public InstantiationService(IServiceCollection container)
        {
            _container = container;
            _container.DisposalLock = DisposalLock.Ignore;
        }

        #endregion

        #region Exposed Methods

        public object GetInstance(Type type)
        {
            return _container.GetService(type);
        }

        public T GetInstance<T>()
        {
            return _container.GetService<T>();
        }

        public void Dispose()
        {
            _container.DisposalLock = DisposalLock.Unlocked;
            _container.Dispose();
        }

        #endregion
    }
}
