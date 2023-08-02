using System;

namespace DateTimeStringParser;

public interface IDateTimeStringParser
{
    bool TryParse(string input, out DateTime? result);
}