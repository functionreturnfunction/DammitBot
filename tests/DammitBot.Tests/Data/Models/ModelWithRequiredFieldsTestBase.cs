using System;
using DammitBot.Library;
using Microsoft.Data.Sqlite;
using Xunit;

namespace DammitBot.Data.Models;

public abstract class ModelWithRequiredFieldsTestBase<TModel> : ModelTestBase<TModel>
    where TModel : class, IEntity
{
    #region Abstract Methods

    protected abstract Action<TModel>[] GetWaysToInvalidate();

    #endregion

    #region Exposed Methods

    [Fact]
    public virtual void TestCreatingWithMissingRequiredFieldsThrowsException()
    {
        foreach (var fn in GetWaysToInvalidate())
        {
            _target = CreateTarget();

            fn(_target);

            Assert.Throws<SqliteException>(() => {
                WithUnitOfWork(uow => uow.Insert<TModel>(_target));
            });
        }
    }

    [Fact]
    public virtual void Test_Updating_WithMissingRequiredFields_ThrowsException()
    {
        foreach (var fn in GetWaysToInvalidate())
        {
            _target = CreateValidObject();

            fn(_target);

            Assert.Throws<SqliteException>(() => {
                WithUnitOfWork(uow => {
                    uow.Update<TModel>(_target);
                    uow.Commit();
                });
            });
        }
    }

    #endregion
}