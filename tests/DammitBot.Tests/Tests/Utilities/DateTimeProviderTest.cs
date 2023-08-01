using System;
using DateTimeStringParser;
using Xunit;

namespace DammitBot.Tests.Utilities
{
    public class DateTimeProviderTest
    {
        [Fact]
        public void TestGetCurrentTimeReturnsCurrentTime()
        {
            var before = DateTime.Now;
            var current = new DateTimeProvider().GetCurrentTime();
            var after = DateTime.Now;
            Assert.InRange(current, before, after);
        }
    }
}