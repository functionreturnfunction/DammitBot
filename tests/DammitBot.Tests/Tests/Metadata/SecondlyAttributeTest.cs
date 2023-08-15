using System;
using System.Linq.Expressions;
using DammitBot.Library;
using DammitBot.Metadata;

namespace DammitBot.Tests.Metadata;

public class SecondlyAttributeTest : IntervalAttributeTestBase<SecondlyAttribute>
{
    protected override int ExpectedInterval => 1;
    protected override IntervalType ExpectedIntervalType => IntervalType.Secondly;
    protected override SecondlyAttribute CreateInterval() => new();

    protected override SecondlyAttribute CreateInterval(int arg) => new(arg);

    protected override Expression<Action<ISimpleScheduleBuilder>> GetExpectedIntervalCall(int interval)
    {
        return b => b.WithIntervalInSeconds(interval);
    }
}