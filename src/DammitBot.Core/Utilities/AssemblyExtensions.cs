using System;
using System.IO;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace DammitBot.Utilities.AssemblyExtensions
{
    public static class AssemblyExtensions
    {
        #region Exposed Methods

        public static string GetDirectory(this Assembly that)
        {
            return Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(that.CodeBase).Path));
        }

        #endregion
    }
}
