using System;
using System.Linq.Expressions;
using DammitBot.Library;
using DammitBot.Metadata;

namespace DammitBot.Tests.Metadata;

public class DailyAttributeTest : IntervalAttributeTestBase<DailyAttribute>
{
    protected override int ExpectedInterval => 24;
    protected override IntervalType ExpectedIntervalType => IntervalType.Hourly;
    protected override DailyAttribute CreateInterval() => new();

    protected override DailyAttribute CreateInterval(int arg) => new(arg);

    protected override Expression<Action<ISimpleScheduleBuilder>> GetExpectedIntervalCall(int interval)
    {
        return b => b.WithIntervalInHours(interval);
    }
}