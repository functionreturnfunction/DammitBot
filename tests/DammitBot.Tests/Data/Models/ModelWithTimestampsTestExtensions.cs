using System;
using DammitBot.Library;
using Xunit;

namespace DammitBot.Data.Models;

public static class ModelWithTimestampsTestExtensions
{
    public static void TestSaveWithValidFieldsSetsCreatedAt<TModel>(this ModelTestBase<TModel> that)
        where TModel : class, IEntityWithTimestamps, new()
    {
        var model = that.CreateValidObject();

        Assert.Equal(that.Now, model.CreatedAt);
        Assert.Null(model.UpdatedAt);
    }

    public static void TestUpdateWithValidFieldsSetsUpdatedAt<TModel>(
        this ModelTestBase<TModel> that,
        Action<TModel> updateModel)
        where TModel : class, IEntityWithTimestamps, new()
    {
        var model = that.CreateValidObject();

        updateModel(model);
        model = that.SaveUpdatedObject(model);

        Assert.Equal(that.Now, model.UpdatedAt);
    }
}