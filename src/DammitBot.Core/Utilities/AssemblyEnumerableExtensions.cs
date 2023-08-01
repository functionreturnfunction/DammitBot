using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DammitBot.Utilities
{
    public static class AssemblyEnumerableExtensions
    {
        #region Exposed Methods

        public static IEnumerable<Type> GetTypes(this IEnumerable<Assembly> that)
        {
            return that.SelectMany(assembly => assembly.GetTypes());
        }

        #endregion
    }
}