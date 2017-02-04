using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StructureMap;

namespace DammitBot.TestLibrary
{
    public abstract class UnitTestBase<TTarget>
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

        [TestInitialize]
        public virtual void TestInitialize()
        {
            _container = CreateContainer();
            Inject(out _log);

            ConfigureContainer();

            _target = ConstructTarget();
        }

        [TestCleanup]
        public virtual void TestCleanup() {}

        #endregion
    }
}
