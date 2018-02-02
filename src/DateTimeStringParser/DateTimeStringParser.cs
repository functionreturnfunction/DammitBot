using System;
using System.Linq;
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
                {new Regex(@"^tomorrow( morning)?$"), ParseTomorrow},
                {new Regex(@"^in (.+) minutes?$"), ParseXMinutes},
                {new Regex(@"^in (.+) hours?"), ParseXHours},
                {new Regex(@"^in (.+) days?"), ParseXDays},
                {new Regex(@"^at ([01]?[0-9]|2[0-3])(?::([0-5]?[0-9]))?$"), ParseTime}
            };

        }

        private DateTime? ParseTime(Match arg)
        {
            return arg.Groups[2].Success
                ? FromNow(dt => dt.GetNext(int.Parse(arg.Groups[1].Value), int.Parse(arg.Groups[2].Value)))
                : FromNow(dt => dt.GetNext(int.Parse(arg.Groups[1].Value)));
        }

        private DateTime? ParseXDays(Match arg)
        {
            return ParseXX(arg, (dt, i) => dt.AddDays(i));
        }

        private DateTime? ParseXHours(Match arg)
        {
            return ParseXX(arg, (dt, i) => dt.AddHours(i));
        }

        private DateTime? ParseXMinutes(Match arg)
        {
            return ParseXX(arg, (dt, i) => dt.AddMinutes(i));
        }

        private DateTime? ParseXX(Match match, Func<DateTime, int, DateTime> fn)
        {
            return FromNow(dt => fn(dt, ParseNumberString(match.Groups[1].Value)));
        }

        private int ParseNumberString(string value)
        {
            return int.Parse(value);
        }

        private DateTime FromNow(Func<DateTime, DateTime> fn)
        {
            return fn(_dateTimeProvider.GetCurrentTime());
        }

        private DateTime? ParseTomorrow(Match match)
        {
            return match.Groups[1].Success ? (DateTime?)null : FromNow(dt => dt.AddDays(1));
        }

        #endregion

        #region Exposed Methods

        public bool TryParse(string input, out DateTime? result)
        {
            var matching = _parseDictionary.Keys.Where(k => k.IsMatch(input));

            if (!matching.Any())
            {
                result = null;
                return false;
            }

            var rgx = matching.First();
            var match = rgx.Match(input);
            result = _parseDictionary[rgx](match);
            return true;
        }

        #endregion
    }


}
