using System;
using DammitBot.Library;
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
        [InlineData("in 1 minute", 1)]
        [InlineData("in 2 minutes", 2)]
        public void TestInXMinutesDoesThatThing(string input, int expected)
        {
            TestTryParse(input, _now.AddMinutes(expected));
        }

        [Theory]
        [InlineData("in 1 hour", 1)]
        [InlineData("in 3 hours", 3)]
        public void TestInXHoursDoesThatThing(string input, int expected)
        {
            TestTryParse(input, _now.AddHours(expected));
        }

        [Theory]
        [InlineData("in 1 day", 1)]
        [InlineData("in 3 days", 3)]
        public void TestInXDaysDoesThatThing(string input, int expected)
        {
            TestTryParse(input, _now.AddDays(expected));
        }

        [Fact]
        public void TestTomorrowReturnsTomorrow()
        {
            TestTryParse("tomorrow", _now.AddDays(1));
        }

        [Theory]
        [InlineData("at 10:30", 10, 30)]
        [InlineData("at 12", 12, 00)]
        public void TestAtXDoesThatThing(string input, int hour, int minute)
        {
            TestTryParse(input, _now.GetNext(hour, minute));
        }
        
        #endregion
    }
}
