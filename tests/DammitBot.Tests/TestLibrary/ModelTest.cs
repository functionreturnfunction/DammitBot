using System;
using DammitBot.Data.Library;
using Xunit;

namespace DammitBot.TestLibrary
{
    public abstract class ModelTest<TModel> : InMemoryDatabaseUnitTestBase<TModel>
        where TModel : class, new()
    {
        #region Properties

        public DateTime Now => _now;

        #endregion

        #region Private Methods

        protected override TModel ConstructTarget()
        {
            return new TModel();
        }

        #endregion

        #region Abstract Methods

        protected abstract void RunPostCreationAssertions(TModel createdObject);

        #endregion

        #region Exposed Methods

        [Fact]
        public virtual void TestThereAreNoneByDefault()
        {
            WithUnitOfWork(uow => Assert.Empty(uow.GetRepository<TModel>()));
        }

        public TModel CreateValidObject()
        {
            dynamic valid;
            valid = ConstructTarget();

            WithUnitOfWork(uow => valid.Id = Convert.ToInt32(uow.GetRepository<TModel>().Insert(valid)));

            _target = ConstructTarget();

            return valid;
        }

        [Fact]
        public void TestCreatingWorksAsExpected()
        {
            var validObject = CreateValidObject();

            Assert.NotNull(validObject);
            RunPostCreationAssertions(validObject);
        }

        public TModel SaveUpdatedObject(TModel model)
        {
            WithUnitOfWork(uow => uow.GetRepository<TModel>().Update(model));

            return model;
        }

        #endregion
    }
}