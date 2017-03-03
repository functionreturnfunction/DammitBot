using System;
using log4net;
using Moq;
using StructureMap;

namespace DammitBot.TestLibrary
{
    public abstract class UnitTestBase<TTarget> : IDisposable
    {
        #region Private Members

        protected IContainer _container;
        protected Mock<ILog> _log;
        protected TTarget _target;

        #endregion

        #region Private Methods

        protected virtual IContainer CreateContainer()
        {
            return new Container();
        }

        protected virtual void ConfigureContainer() {}

        protected virtual void Inject<TMock>(out Mock<TMock> obj, MockBehavior behavior = MockBehavior.Default)
            where TMock : class
        {
            Inject((obj = new Mock<TMock>(behavior)).Object);
        }

        protected virtual void Inject<TMock>()
            where TMock : class
        {
            Mock<TMock> throwaway;
            Inject(out throwaway);
        }

        protected virtual void Inject<TObj>(TObj obj)
            where TObj : class
        {
            _container.Inject(obj);
        }

        protected virtual TTarget ConstructTarget()
        {
            return _container.GetInstance<TTarget>();
        }

        #endregion

        #region Setup/Teardown

        public UnitTestBase()
        {
            _container = CreateContainer();
            Inject(out _log);

            ConfigureContainer();

            _target = ConstructTarget();
        }

        public virtual void Dispose() {}

        #endregion
    }
}
