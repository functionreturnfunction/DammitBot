using System;
using DammitBot.Data.Models;
using DammitBot.TestLibrary;
using Xunit;

namespace DammitBot.Data.Dapper
{
    public class NickTest : ModelWithRequiredFieldsTest<Nick>
    {
        #region Constants

        public struct Defaults
        {
            public const string NICKNAME = "nick";
        }

        #endregion

        #region Private Methods

        protected override Nick GetValidObject()
        {
            return ConstructValidObject();
        }

        protected override Action<Nick>[] GetWaysToInvalidate()
        {
            return new Action<Nick>[] {
                n => n.Nickname = null
            };
        }

        protected override void RunPostCreationAssertions(Nick createdObject)
        {
            Assert.InRange(createdObject.Id, 1, int.MaxValue);
            Assert.Equal(Defaults.NICKNAME, createdObject.Nickname);
        }

        #endregion

        #region Exposed Methods

        public static Nick ConstructValidObject()
        {
            return new Nick {
                Nickname = Defaults.NICKNAME
            };
        }

        [Fact]
        public void TestCreateTimestamp()
        {
            this.TestSaveWithValidFieldsSetsCreatedAt();
        }

        [Fact]
        public void TestUpdateTimestamp()
        {
            this.TestUpdateWithValidFieldsSetsUpdatedAt(n => n.Nickname = "new nickname");
        }

        [Fact]
        public void TestUpdatingWithUserWorks()
        {
            var validObject = CreateValidObject();

            using (_target)
            {
                validObject.User = UserTest.ConstructValidObject();
                validObject.User.Id = Convert.ToInt32(_target.Insert(validObject.User));

                _target.Insert(validObject);

                Assert.InRange(_target.Find<Nick>(validObject.Id).User.Id, 1, int.MaxValue);
            }
        }

        #endregion
    }
}
