using System;
using System.Linq.Expressions;
using DammitBot.Library;
using DammitBot.Metadata;

namespace DammitBot.Tests.Metadata;

public class MinutelyAttributeTest : IntervalAttributeTestBase<MinutelyAttribute>
{
    protected override int ExpectedInterval => 1;
    protected override IntervalType ExpectedIntervalType => IntervalType.Minutely;
    protected override MinutelyAttribute CreateInterval() => new();

    protected override MinutelyAttribute CreateInterval(int arg) => new(arg);

    protected override Expression<Action<ISimpleScheduleBuilder>> GetExpectedIntervalCall(int interval)
    {
        return b => b.WithIntervalInMinutes(interval);
    }
}