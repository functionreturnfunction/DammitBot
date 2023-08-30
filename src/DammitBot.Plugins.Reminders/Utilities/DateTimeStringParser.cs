using System;
using System.Text.RegularExpressions;
using Chronic.Core;
using DateTimeProvider;

namespace DammitBot.Utilities;

/// <summary>
/// Parser for strings, which attempts to derive <see cref="DateTime"/>s from them.
/// </summary>
public partial class DateTimeStringParser
{
    #region Private Members
    
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly Parser _parser;
    
    #endregion
    
    #region Constructors

    /// <summary>
    /// Constructor for the <see cref="DateTimeStringParser"/> class.
    /// </summary>
    public DateTimeStringParser(IDateTimeProvider dateTimeProvider, Parser parser)
    {
        _dateTimeProvider = dateTimeProvider;
        _parser = parser;
    }
    
    #endregion

    #region Private Methods

    [GeneratedRegex("^at ")]
    private static partial Regex BeginsWithAt();
    
    private string Sanitize(string phrase)
    {
        return BeginsWithAt().Replace(phrase, "");
    }
    
    #endregion
    
    #region Exposed Methods

    /// <summary>
    /// Parse the given string <paramref name="phrase"/> for a <see cref="DateTime"/> relative to the
    /// current.  If the string was parsable to a relative <see cref="DateTime"/> value, that value will
    /// be returned, else null.
    /// </summary>
    public DateTime? Parse(string phrase)
    {
        var now = _dateTimeProvider.GetCurrentTime();
        
        if (phrase == "tomorrow")
        {
            return now.AddDays(1);
        }
        
        var when = _parser.Parse(
            Sanitize(phrase),
            new Options {Clock = () => now});

        var start = when?.Start; 
        
        return start < now ? start.Value.AddDays(1) : start;
    }
    
    #endregion
}