using DammitBot.Data.Library;
using Xunit;

namespace DammitBot.TestLibrary
{
    public static class ModelWithTimestampsTestExtensions
    {
        public static void TestSaveWithValidFieldsSetsCreatedAt<TModel>(this ModelTest<TModel> that)
            where TModel : IThingWithTimestamps, new()
        {
            var model = that.CreateValidObject();

            Assert.Equal(that.Now, model.CreatedAt);
            Assert.Null(model.UpdatedAt);
        }
    }
}