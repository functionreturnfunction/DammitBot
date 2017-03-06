﻿using DammitBot.Data.Models;
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

            Assert.Equal($"@{user.Username} you wanted me to remind you to do stuff and junk",
                _target.Generate(new Reminder {Text = "to do stuff and junk", From = user, To = user}).Text);
        }

        #endregion
    }
}
