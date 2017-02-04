using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace DammitBot.Utilities.AssemblyEnumerableExtensions
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