using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DateTimeStringParser
{
    public class DateTimeStringParser : IDateTimeStringParser
    {
        #region Private Members

        protected readonly IDateTimeProvider _dateTimeProvider;
        protected readonly Dictionary<Regex, Func<Match, DateTime?>> _parseDictionary;

        #endregion

        #region Constructors

        public DateTimeStringParser(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
            _parseDictionary = new Dictionary<Regex, Func<Match, DateTime?>> {
                {new Regex(@"^tomorrow( morning)?$"), ParseTomorrow}
            };
            
        }

        private DateTime? ParseTomorrow(Match match)
        {
            var now = _dateTimeProvider.GetCurrentTime();
            return match.Groups[1].Success ? (DateTime?)null : now.AddDays(1);
        }

        #endregion

        #region Exposed Methods

        public bool TryParse(string input, out DateTime? result)
        {
            foreach (var rgx in _parseDictionary.Keys)
            {
                Match match;
                if ((match = rgx.Match(input)).Success)
                {
                    result = _parseDictionary[rgx](match);
                    return true;
                }
            }
            result = null;
            return false;
        }

        #endregion
    }

    
}
