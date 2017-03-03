using System;
using System.Linq;
using System.Linq.Expressions;
using DammitBot.Data.Models;
using DammitBot.TestLibrary;
using Moq;
using Xunit;

namespace DammitBot.Data.Library
{
    public class RepositoryTest : UnitTestBase<Repository<Nick>>
    {
        private Mock<IDataCommandHelper> _dataCommandHelper;

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            Inject(out _dataCommandHelper);
        }

        [Fact]
        public void TestSaveSavesAndReturnsEntity()
        {
            var entity = new Nick();
            
            Assert.Same(entity, _target.Save(entity));

            _dataCommandHelper.Verify(x => x.Save(entity));
        }

        [Fact]
        public void TestFindLoadsEntity()
        {
            var entity = new Nick();
            _dataCommandHelper.Setup(x => x.Load<Nick>("foo")).Returns(entity);

            Assert.Same(entity, _target.Find("foo"));
        }

        [Fact]
        public void TestWhereRunsSearch()
        {
            var entity = new Nick {Id = 1};
            _dataCommandHelper.Setup(x => x.Where(It.Is<Expression<Func<Nick, bool>>>(e => e.Compile()(entity))))
                .Returns(new[] {entity}.AsQueryable());

            var result = _target.Where(n => n.Id == 1);

            Assert.Same(entity, result.Single());

        }
    }
}
