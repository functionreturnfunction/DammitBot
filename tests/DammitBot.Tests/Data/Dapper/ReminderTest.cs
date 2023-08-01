using System;
using DammitBot.Data.Models;
using DammitBot.Library;
using DammitBot.TestLibrary;
using Xunit;

namespace DammitBot.Data.Dapper
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

        protected override Reminder ConstructTarget()
        {
            var user = UserTest.ConstructValidObject();
            WithUnitOfWork(uow => {
                user.Id = Convert.ToInt32(uow.Insert<User>(user));
                uow.Commit();
            });
            return ConstructValidObject(user, user, _now);
        }

        protected override Action<Reminder>[] GetWaysToInvalidate()
        {
            return new Action<Reminder>[] {
                r => r.RemindAt = null,
                r => {
                    r.From = null;
                    r.FromId = null;
                },
                r => {
                    r.To = null;
                    r.ToId = null;
                }
            };
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
        public override void TestThereAreNoneByDefault()
        {
            base.TestThereAreNoneByDefault();
        }

        [Fact]
        public void TestCreateTimestamp()
        {
            this.TestSaveWithValidFieldsSetsCreatedAt();
        }

        [Fact]
        public void TestUpdateTimestamp()
        {
            this.TestUpdateWithValidFieldsSetsUpdatedAt(u => u.Text = "new reminder");
        }

        [Fact]
        public override void TestUpdatingWithMissingRequiredFieldsThrowsException()
        {
            base.TestUpdatingWithMissingRequiredFieldsThrowsException();
        }

        #endregion
    }
}