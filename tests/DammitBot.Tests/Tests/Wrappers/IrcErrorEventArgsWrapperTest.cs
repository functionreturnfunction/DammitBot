using System;
using DammitBot.Library;
using DammitBot.Wrappers;
using IrcDotNet;
using Xunit;

namespace DammitBot.Tests.Wrappers;

public class IrcErrorEventArgsWrapperTest : UnitTestBase<IrcErrorEventArgsWrapper>
{
    private Exception? _wrappedException;
    
    protected override IrcErrorEventArgsWrapper ConstructTarget()
    {
        return new IrcErrorEventArgsWrapper(new IrcErrorEventArgs(_wrappedException = new Exception()));
    }

    [Fact]
    public void Test_Exception_ReturnsWrappedException()
    {
        Assert.Equal(_wrappedException!, _target.Exception);
    }
}