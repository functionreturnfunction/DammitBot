using Bogus;

namespace DammitBot.Data.Models.Fakers;

public class NickFaker : Faker<Nick>
{
    public NickFaker()
    {
        RuleFor(n => n.Nickname, f => f.Internet.UserName());
    }
}