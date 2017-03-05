using System;
using NHibernate;
using Xunit;

namespace DammitBot.TestLibrary
{
    public abstract class ModelWithRequiredFieldsTest<TModel> : ModelTest<TModel>
        where TModel : new()
    {
        #region Private Methods

        protected override TModel GetValidObject()
        {
            throw new InvalidOperationException(
                "This method must be overridden in inheriting classes to provide a constructed model with valid fields for saving");
        }

        #endregion

        #region Exposed Methods

        [Fact]
        public void TestCreatingWithMissingRequiredFieldsThrowsException()
        {
            Assert.Throws<PropertyValueException>(() => _target.Save(new TModel()));
        }

        #endregion
    }
}