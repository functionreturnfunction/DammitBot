using Bogus;

namespace DammitBot.Data.Models.Fakers;

public class UserFaker : Faker<User>
{
    public UserFaker()
    {
        RuleFor(u => u.Username, f => f.Internet.UserName());
    }
}