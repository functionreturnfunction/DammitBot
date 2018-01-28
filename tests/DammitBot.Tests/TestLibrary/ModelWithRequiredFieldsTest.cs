﻿using System;
using Xunit;

namespace DammitBot.TestLibrary
{
    public abstract class ModelWithRequiredFieldsTest<TModel> : ModelTest<TModel>
        where TModel : class, new()
    {
        #region Private Methods

        protected override TModel GetValidObject()
        {
            throw new InvalidOperationException(
                "This method must be overridden in inheriting classes to provide a constructed model with valid fields for saving");
        }

        #endregion

        #region Abstract Methods

        protected abstract Action<TModel>[] GetWaysToInvalidate();

        #endregion

        #region Exposed Methods

        [Fact]
        public void TestCreatingWithMissingRequiredFieldsThrowsException()
        {
            foreach (var fn in GetWaysToInvalidate())
            {
                _target = ConstructTarget();
                var obj = GetValidObject();

                fn(obj);

                Assert.Throws<InvalidOperationException>(() => {
                    _target.Insert(obj);
                    _target.Dispose();
                });
            }
        }

        [Fact]
        public void TestUpdatingWithMissingRequiredFieldsThrowsException()
        {
            foreach (var fn in GetWaysToInvalidate())
            {
               _target = ConstructTarget();
                var obj = CreateValidObject();

                fn(obj);

                Assert.Throws<InvalidOperationException>(() => {
                    _target.Update(obj);
                    _target.Dispose();
                });
            }
        }

        #endregion
    }
}