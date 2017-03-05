using System;

namespace DateTimeStringParser
{
    public class DateTimeStringParser : IDateTimeStringParser
    {
        protected readonly IDateTimeProvider _dateTimeProvider;

        public DateTimeStringParser(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public bool TryParse(string input, out DateTime? result)
        {
            throw new NotImplementedException();
        }
    }
}
