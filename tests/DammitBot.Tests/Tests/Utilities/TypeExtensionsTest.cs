using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using DammitBot.Utilities;
using Xunit;
using Xunit.Sdk;

namespace DammitBot.Tests.Utilities;

public class TypeExtensionsTest
{
    [Fact]
    public void Test_HasProperty_ReturnsTrueAndSetsPropertyInfo_IfTypeHasProperty()
    {
        PropertyInfo prop;

        Assert.True(typeof(Type).HasProperty("Assembly", out prop));
        Assert.Equal("Assembly", prop.Name);

        Assert.True(typeof(Type).HasProperty<Assembly>("Assembly", out prop));
        Assert.Equal("Assembly", prop.Name);
    }

    [Fact]
    public void Test_HasProperty_ReturnsFalse_IfTypeDoesNotHaveProperty()
    {
        PropertyInfo prop;

        Assert.False(typeof(Type).HasProperty("APropertyIDoNoHave", out prop));
        Assert.Null(prop);

        Assert.False(typeof(Type).HasProperty<Assembly>("APropertyIDoNoHave", out prop));
        Assert.Null(prop);
    }
}