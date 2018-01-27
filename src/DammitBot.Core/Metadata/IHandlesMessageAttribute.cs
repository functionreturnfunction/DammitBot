using System.Text.RegularExpressions;

namespace DammitBot.Metadata
{
    public interface IHandlesMessageAttribute
    {
        Regex Regex { get; }
    }
}