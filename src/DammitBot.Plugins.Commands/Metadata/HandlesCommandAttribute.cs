namespace DammitBot.Metadata
{
    public class HandlesCommandAttribute : HandlesMessageAttribute, IHandlesCommandAttribute
    {
        public HandlesCommandAttribute(string rgx) : base(rgx) {}
    }
}