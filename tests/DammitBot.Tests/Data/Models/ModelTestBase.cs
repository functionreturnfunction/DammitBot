using System;
using DammitBot.Library;
using Xunit;

namespace DammitBot.Data.Models;

public abstract class ModelTestBase<TModel> : InMemoryDatabaseUnitTestBase<TModel>
    where TModel : class, IEntity
{
    #region Properties

    public DateTime Now => _now;

    #endregion

    #region Abstract Methods

    protected abstract void RunPostCreationAssertions(TModel createdObject);

    #endregion

    #region Exposed Methods

    public TModel CreateValidObject()
    {
        var valid = ConstructTarget();

        WithUnitOfWork(uow => {
            valid.Id = Convert.ToInt32(uow.Insert(valid));
            uow.Commit();
        });

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
        WithUnitOfWork(uow => {
            uow.Update<TModel>(model);
            uow.Commit();
        });

        return model;
    }

    #endregion
}