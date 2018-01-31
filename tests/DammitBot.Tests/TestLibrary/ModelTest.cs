using System;
using DammitBot.Data.Library;
using Xunit;

namespace DammitBot.TestLibrary
{
    public abstract class ModelTest<TModel> : InMemoryDatabaseUnitTestBase<PersistenceService>
        where TModel : class, new()
    {
        #region Properties

        public DateTime Now => _now;

        #endregion

        #region Private Methods

        protected virtual TModel GetValidObject()
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
            using (_target)
            {
                Assert.Empty(_target.Query<TModel>());
            }
        }

        public TModel CreateValidObject()
        {
            dynamic valid;
            using (_target)
            {
                valid = GetValidObject();
                valid.Id = Convert.ToInt32(_target.Insert(valid));
            }

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
            using (_target)
            {
                _target.Update(model);
            }

            return model;
        }

        #endregion
    }
}