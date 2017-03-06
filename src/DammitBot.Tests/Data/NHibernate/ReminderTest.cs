using System;
using DammitBot.Data.Models;
using DammitBot.TestLibrary;
using Xunit;

namespace DammitBot.Data.NHibernate
{
    public class ReminderTest : ModelWithRequiredFieldsTest<Reminder>
    {
        #region Constants

        public struct Defaults
        {
            #region Constants

            public const string TEXT = "reminder";

            #endregion
        }

        #endregion

        #region Private Methods

        protected override Reminder GetValidObject()
        {
            var user = _target.Save(UserTest.ConstructValidObject());
            return ConstructValidObject(user, user, _now);
        }

        protected override void RunPostCreationAssertions(Reminder createdObject)
        {
            Assert.InRange(createdObject.Id, 1, int.MaxValue);
            Assert.Equal(_now, createdObject.RemindAt.Value);
            Assert.Null(createdObject.RemindedAt);
        }

        #endregion

        #region Exposed Methods

        public static Reminder ConstructValidObject(User from, User to, DateTime remindAt)
        {
            return new Reminder {
                Text = Defaults.TEXT,
                From = from,
                To = to,
                RemindAt = remindAt
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