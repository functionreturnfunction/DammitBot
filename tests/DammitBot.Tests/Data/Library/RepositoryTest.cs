using System.Collections.Generic;
using System.Linq;
using DammitBot.Library;
using DammitBot.Models;
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
        public void TestInsertSavesAndReturnsIdentifier()
        {
            var entity = new Nick();

            _dataCommandHelper.Setup(x => x.Insert(entity)).Returns(666);
            
            Assert.Equal(666, _target.Insert(entity));

            _dataCommandHelper.Verify(x => x.Insert(entity));
        }

        [Fact]
        public void TestFindLoadsEntity()
        {
            var entity = new Nick();
            _dataCommandHelper.Setup(x => x.Load<Nick>(666)).Returns(entity);

            Assert.Same(entity, _target.Find(666));
        }

        [Fact]
        public void TestWhereRunsSearch()
        {
            var entity = new Nick {Id = 1};
            _dataCommandHelper.Setup(x => x.GetQueryable<Nick>()).Returns(new List<Nick> {entity}.AsQueryable());

            var result = _target.Where(n => n.Id == 1);

            Assert.Same(entity, result.Single());

        }
    }
}
