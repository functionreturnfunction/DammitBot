using DammitBot.Data.Models;
using DammitBot.TestLibrary;
using Xunit;

namespace DammitBot.Utilities
{
    public class ReminderTextGeneratorTest : UnitTestBase<ReminderTextGenerator>
    {
        #region Exposed Methods

        [Fact]
        public void TestGenerateGeneratesTextForSameUser()
        {
            var user = new User {
                Username = "foo"
            };

            Assert.Equal($"@{user.Username} you wanted me to remind you to do stuff and junk on {_now:d} at {_now:t}",
                _target.Generate(new Reminder {
                    Text = "to do stuff and junk", From = user, To = user, RemindAt = _now
                }).Text);
        }

        [Fact]
        public void TestGenerateGeneratesTextForDifferentUser()
        {
            var user = new User {
                Username = "foo"
            };
            var differentUser = new User {
                Username = "bar"
            };

            Assert.Equal($"@{differentUser.Username} {user.Username} wanted me to remind you to do stuff and junk on {_now:d} at {_now:t}",
                _target.Generate(new Reminder {
                    Text = "to do stuff and junk", From = user, To = differentUser, RemindAt = _now
                }).Text);
        }

        #endregion
    }
}
