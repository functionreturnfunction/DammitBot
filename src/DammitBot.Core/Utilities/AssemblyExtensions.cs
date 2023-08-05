using System;
using System.IO;
using System.Reflection;

namespace DammitBot.Utilities;

public static class AssemblyExtensions
{
    #region Exposed Methods

    public static string GetDirectory(this Assembly that)
    {
        return Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(that.Location).Path))!;
    }

    #endregion
}