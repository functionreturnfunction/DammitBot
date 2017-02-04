using System;
using DammitBot.TestLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StructureMap;

namespace DammitBot.Wrappers
{
    [TestClass]
    public class InstantiationServiceTest : UnitTestBase<InstantiationService>
    {
        #region Private Members

        private Mock<IContainer> _mockContainer;

        #endregion

        protected override IContainer CreateContainer()
        {
            _mockContainer = new Mock<IContainer>();
            return _mockContainer.Object;
        }

        protected override InstantiationService ConstructTarget()
        {
            return new InstantiationService(_mockContainer.Object);
        }

        [TestMethod]
        public void TestDisposeDisposesContainer()
        {
            _target.Dispose();

            _mockContainer.Verify(x => x.Dispose());
        }

        [TestMethod]
        public void TestGenericGetInstanceGenericallyGetsInstanceFromContainer()
        {
            var now = DateTime.Now;
            _mockContainer.Setup(x => x.GetInstance<DateTime>()).Returns(now);

            Assert.AreEqual(now, _target.GetInstance<DateTime>());
        }

        [TestMethod]
        public void TestGetInstanceGetsInstanceFromContainer()
        {
            var now = DateTime.Now;
            _mockContainer.Setup(x => x.GetInstance(typeof(DateTime))).Returns(now);

            Assert.AreEqual(now, _target.GetInstance(typeof(DateTime)));
        }
    }
}
