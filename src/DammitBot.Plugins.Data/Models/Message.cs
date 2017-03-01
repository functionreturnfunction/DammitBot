namespace DammitBot.Data.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Nick From { get; set; }
    }
}