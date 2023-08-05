using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DateTimeStringParser;

/// <inheritdoc cref="IDateTimeStringParser"/>
public class DateTimeStringParser : IDateTimeStringParser
{
    #region Private Members

    private readonly Dictionary<Regex, Func<DateTime, Match, DateTime?>> _parseDictionary;

    #endregion

    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="DateTimeStringParser"/> class.
    /// </summary>
    public DateTimeStringParser()
    {
        _parseDictionary = new Dictionary<Regex, Func<DateTime, Match, DateTime?>> {
            {new Regex(@"^tomorrow( morning)?$"), ParseTomorrow},
            {new Regex(@"^in (.+) minutes?$"), ParseXMinutes},
            {new Regex(@"^in (.+) hours?"), ParseXHours},
            {new Regex(@"^in (.+) days?"), ParseXDays},
            {new Regex(@"^at ([01]?[0-9]|2[0-3])(?::([0-5]?[0-9]))?$"), ParseTime}
        };

    }
    
    #endregion
    
    #region Private Methods

    private DateTime? ParseTime(DateTime now, Match arg)
    {
        return arg.Groups[2].Success
            ? FromNow(
                now,
                dt => dt.GetNext(int.Parse(arg.Groups[1].Value),
                    int.Parse(arg.Groups[2].Value)))
            : FromNow(now, dt => dt.GetNext(int.Parse(arg.Groups[1].Value)));
    }

    private DateTime? ParseXDays(DateTime now, Match arg)
    {
        return ParseXX(now, arg, (dt, i) => dt.AddDays(i));
    }

    private DateTime? ParseXHours(DateTime now, Match arg)
    {
        return ParseXX(now, arg, (dt, i) => dt.AddHours(i));
    }

    private DateTime? ParseXMinutes(DateTime now, Match arg)
    {
        return ParseXX(now, arg, (dt, i) => dt.AddMinutes(i));
    }

    private DateTime? ParseXX(DateTime now, Match match, Func<DateTime, int, DateTime> fn)
    {
        return FromNow(now, dt => fn(dt, ParseNumberString(match.Groups[1].Value)));
    }

    private int ParseNumberString(string value)
    {
        return int.Parse(value);
    }

    private DateTime FromNow(DateTime now, Func<DateTime, DateTime> fn)
    {
        return fn(now);
    }

    private DateTime? ParseTomorrow(DateTime now, Match match)
    {
        return match.Groups[1].Success ? (DateTime?)null : FromNow(now, dt => dt.AddDays(1));
    }

    #endregion

    #region Exposed Methods

    /// <inheritdoc cref="IDateTimeStringParser.TryParse"/>
    public bool TryParse(DateTime now, string input, out DateTime? result)
    {
        var matching = _parseDictionary.Keys.Where(k => k.IsMatch(input));

        if (!matching.Any())
        {
            result = null;
            return false;
        }

        var rgx = matching.First();
        var match = rgx.Match(input);
        result = _parseDictionary[rgx](now, match);
        return true;
    }

    #endregion
}