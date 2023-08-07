using System.IO;
using System.Reflection;

namespace DammitBot.Utilities;

/// <summary>
/// Extensions to the <see cref="Assembly"/> class.
/// </summary>
public static class AssemblyExtensions
{
    #region Exposed Methods

    /// <summary>
    /// Returns the directory path at which the assembly is stored.
    /// </summary>
    public static string GetDirectory(this Assembly that)
    {
        return Path.GetDirectoryName(that.Location)!;
    }

    #endregion
}