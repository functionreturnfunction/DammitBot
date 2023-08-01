using System;
using DammitBot.Library;
using DammitBot.Wrappers;
using Moq;
using StructureMap;
using Xunit;

namespace DammitBot.Tests.Wrappers
{
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

        [Fact]
        public void TestDisposeDisposesContainer()
        {
            _target.Dispose();

            _mockContainer.Verify(x => x.Dispose());
        }

        [Fact]
        public void TestGenericGetInstanceGenericallyGetsInstanceFromContainer()
        {
            var now = DateTime.Now;
            _mockContainer.Setup(x => x.GetInstance<DateTime>()).Returns(now);

            Assert.Equal(now, _target.GetInstance<DateTime>());
        }

        [Fact]
        public void TestGetInstanceGetsInstanceFromContainer()
        {
            var now = DateTime.Now;
            _mockContainer.Setup(x => x.GetInstance(typeof(DateTime))).Returns(now);

            Assert.Equal(now, _target.GetInstance(typeof(DateTime)));
        }
    }
}
