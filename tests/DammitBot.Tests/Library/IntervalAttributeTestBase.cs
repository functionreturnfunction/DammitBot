using System;
using System.Linq.Expressions;
using DammitBot.Metadata;
using Moq;
using Quartz;
using Xunit;

namespace DammitBot.Library;

public abstract class IntervalAttributeTestBase<TInterval>
    where TInterval : IntervalAttribute
{
    protected abstract int ExpectedInterval { get; }
    protected abstract IntervalType ExpectedIntervalType { get; }

    protected abstract TInterval CreateInterval();
    protected abstract TInterval CreateInterval(int arg);

    protected abstract Expression<Action<ISimpleScheduleBuilder>> GetExpectedIntervalCall(int interval);

    [Fact]
    public void Test_ConstructorWithNoArguments_SetsExpectedIntervalAndIntervalType()
    {
        var target = CreateInterval();
        
        Assert.Equal(ExpectedInterval, target.Interval);
        Assert.Equal(ExpectedIntervalType, target.IntervalType);
    }

    [Fact]
    public void Test_ConstructorWithArgument_SetsIntervalToMultipleOfExpected()
    {
        var multiple = 5;
        var target = CreateInterval(multiple);
        
        Assert.Equal(ExpectedInterval * multiple, target.Interval);
        Assert.Equal(ExpectedIntervalType, target.IntervalType);
    }

    [Fact]
    public void Test_SetInterval_SetsExpectedIntervalOnScheduleBuilder()
    {
        var schedulerBuilder = new Mock<ISimpleScheduleBuilder>();
        var target = CreateInterval();

        target.SetInterval(schedulerBuilder.Object);
        
        schedulerBuilder.Verify(GetExpectedIntervalCall(ExpectedInterval));
    }
}