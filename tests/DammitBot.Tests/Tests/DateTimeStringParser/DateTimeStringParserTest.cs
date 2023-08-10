using System;
using DammitBot.Library;
using DateTimeProvider;
using Xunit;

namespace DammitBot.Tests.DateTimeStringParser;

public class DateTimeStringParserTest : UnitTestBase<global::DateTimeStringParser.DateTimeStringParser>
{
    #region Private Methods

    private void TestTryParse(string input, DateTime expected)
    {
        Assert.True(_target.TryParse(_now, input, out var result));
        Assert.NotNull(result);
        Assert.Equal(result!.Value, expected);
    }

    #endregion

    #region Exposed Methods

    [Theory]
    [InlineData("in 1 minute", 1)]
    [InlineData("in 2 minutes", 2)]
    public void Test_InXMinutes_DoesThatThing(string input, int expected)
    {
        TestTryParse(input, _now.AddMinutes(expected));
    }

    [Theory]
    [InlineData("in 1 hour", 1)]
    [InlineData("in 3 hours", 3)]
    public void Test_InXHours_DoesThatThing(string input, int expected)
    {
        TestTryParse(input, _now.AddHours(expected));
    }

    [Theory]
    [InlineData("in 1 day", 1)]
    [InlineData("in 3 days", 3)]
    public void Test_InXDays_DoesThatThing(string input, int expected)
    {
        TestTryParse(input, _now.AddDays(expected));
    }

    [Fact]
    public void Test_Tomorrow_ReturnsTomorrow()
    {
        TestTryParse("tomorrow", _now.AddDays(1));
    }

    [Theory]
    [InlineData("at 10:30", 10, 30)]
    [InlineData("at 12", 12, 00)]
    public void Test_AtX_DoesThatThing(string input, int hour, int minute)
    {
        TestTryParse(input, _now.GetNext(hour, minute));
    }

    [Fact]
    public void Test_UnparsableValue_ReturnsFalse()
    {
        Assert.False(_target.TryParse(_now, "at the end of the universe", out var result));
        Assert.Null(result);
    }

    #endregion
}