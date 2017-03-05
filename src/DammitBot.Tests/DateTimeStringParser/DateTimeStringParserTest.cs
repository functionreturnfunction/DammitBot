using System;
using DammitBot.TestLibrary;
using Xunit;

// ReSharper disable once CheckNamespace
namespace DateTimeStringParser
{
    public class DateTimeStringParserTest : UnitTestBase<DateTimeStringParser>
    {
        #region Private Methods

        private void TestTryParse(string input, DateTime expected)
        {
            DateTime? result;
            Assert.True(_target.TryParse(input, out result));
            Assert.Equal(result.Value, expected);
        }

        #endregion

        #region Exposed Methods

        [Theory]
        [InlineData("in one minute", 1)]
        [InlineData("in two minutes", 2)]
        [InlineData("in 2 minutes", 2)]
        public void TestInXMinutesDoesThatThing(string input, int expected)
        {
            TestTryParse(input, _now.AddMinutes(expected));
        }

        [Theory]
        [InlineData("in one hour", 1)]
        [InlineData("in three hours", 3)]
        [InlineData("in 7 hours", 7)]
        public void TestInXHoursDoesThatThing(string input, int expected)
        {
            TestTryParse(input, _now.AddHours(expected));
        }

        [Theory]
        [InlineData("in one day", 1)]
        [InlineData("in three days", 3)]
        [InlineData("in 7 days", 7)]
        public void TestInXDaysDoesThatThing(string input, int expected)
        {
            TestTryParse(input, _now.AddDays(expected));
        }

        [Fact]
        public void TestTomorrowReturnsTomorrow()
        {
            TestTryParse("tomorrow", _now.AddDays(1));
        }

        #endregion
    }
}
