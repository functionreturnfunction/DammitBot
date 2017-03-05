using DammitBot.Data.Models;
using DammitBot.TestLibrary;
using Xunit;

namespace DammitBot.Data.NHibernate
{
    public class UserTest : ModelWithRequiredFieldsTest<User>
    {
        #region Constants

        public struct Defaults
        {
            #region Constants

            public const string USERNAME = "user";

            #endregion
        }

        #endregion

        #region Private Methods

        protected override void RunPostCreationAssertions(User createdObject)
        {
            Assert.InRange(createdObject.Id, 1, int.MaxValue);
            Assert.Equal(Defaults.USERNAME, createdObject.Username);
        }

        protected override User GetValidObject()
        {
            return ConstructValidObject();
        }

        #endregion

        #region Exposed Methods

        public static User ConstructValidObject()
        {
            return new User {
                Username = Defaults.USERNAME
            };
        }

        [Fact]
        public void TestCreateTimestamp()
        {
            this.TestSaveWithValidFieldsSetsCreatedAt();
        }

        #endregion
    }
}