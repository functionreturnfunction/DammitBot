using System;
using System.Linq.Expressions;
using DammitBot.Library;
using DammitBot.Metadata;

namespace DammitBot.Tests.Metadata;

public class HourlyAttributeTest : IntervalAttributeTestBase<HourlyAttribute>
{
    protected override int ExpectedInterval => 1;
    protected override IntervalType ExpectedIntervalType => IntervalType.Hourly;
    protected override HourlyAttribute CreateInterval() => new();

    protected override HourlyAttribute CreateInterval(int arg) => new(arg);

    protected override Expression<Action<ISimpleScheduleBuilder>> GetExpectedIntervalCall(int interval)
    {
        return b => b.WithIntervalInHours(interval);
    }
}