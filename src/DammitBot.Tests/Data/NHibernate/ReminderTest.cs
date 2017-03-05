using DammitBot.Data.Models;
using DammitBot.TestLibrary;
using Xunit;

namespace DammitBot.Data.NHibernate
{
    public class ReminderTest : ModelWithRequiredFieldsTest<Reminder>
    {
        public struct Defaults
        {
            public const string TEXT = "reminder";
        }

        public static Reminder ConstructValidObject(User from, User to)
        {
            return new Reminder {
                Text = Defaults.TEXT,
                From = from,
                To = to
            };
        }

        protected override Reminder GetValidObject()
        {
            var user = _target.Save(UserTest.ConstructValidObject());
            return ConstructValidObject(user, user);
        }

        protected override void RunPostCreationAssertions(Reminder createdObject)
        {
            Assert.InRange(createdObject.Id, 1, int.MaxValue);
        }

        [Fact]
        public void TestCreateTimestamp()
        {
            this.TestSaveWithValidFieldsSetsCreatedAt();
        }
    }
}