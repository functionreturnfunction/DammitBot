using System;

namespace DateTimeStringParser;

/// <summary>
/// Parser for strings, which attempts to derive <see cref="DateTime"/>s from them.
/// </summary>
public interface IDateTimeStringParser
{
    /// <summary>
    /// Attempt to parse the given <paramref name="input"/> into a <see cref="DateTime"/> relative to the
    /// provided <see cref="DateTime"/> <paramref name="now"/>.  If successful, <paramref name="result"/>
    /// will be set to the <see cref="DateTime"/> value, and true will be returned.  If unsuccessful,
    /// false will be returned.
    /// </summary>
    bool TryParse(DateTime now, string input, out DateTime? result);
}