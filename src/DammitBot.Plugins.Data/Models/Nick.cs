namespace DammitBot.Data.Models
{
    public class Nick
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public User User { get; set; }
    }
}