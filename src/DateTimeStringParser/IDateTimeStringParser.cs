using System;

namespace DateTimeStringParser;

/// <summary>
/// Parser for strings, which attempts to derive <see cref="DateTime"/>s from them.
/// </summary>
public interface IDateTimeStringParser
{
    /// <summary>
    /// Attempt to parse the given <paramref name="input"/> into a <see cref="DateTime"/>.  If successful,
    /// <paramref name="result"/> will be set to the <see cref="DateTime"/> value, and true will be
    /// returned.  If unsuccessful, false will be returned.
    /// </summary>
    bool TryParse(string input, out DateTime? result);
}