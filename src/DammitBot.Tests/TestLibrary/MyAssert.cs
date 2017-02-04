using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DammitBot.TestLibrary
{
    public static class MyAssert
    {
        public static void Throws<TException>(Action toThrow)
            where TException : Exception
        {
            var thrown = true;

            try
            {
                toThrow();
                thrown = false;
            }
            catch (TException) {}
            catch (Exception e)
            {
                Assert.Fail(
                    $"Expected exception of type '{typeof(TException).FullName}', got '{e.GetType().FullName}' instead.");
            }

            Assert.IsTrue(thrown, "Exception was not encountered as expected.");
        }

        public static void Contains<TObj>(IEnumerable<TObj> expected, TObj obj, string message = null)
        {
            Assert.IsTrue(expected.Any(o => o.Equals(obj)), message ?? $"Object was not found in collection: '{obj}'");
        }
    }
}