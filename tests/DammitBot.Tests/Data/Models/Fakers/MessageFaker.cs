using Bogus;

namespace DammitBot.Data.Models.Fakers;

public class MessageFaker : Faker<Message>
{
    public MessageFaker()
    {
        RuleFor(m => m.Text, f => f.Lorem.Text());
        RuleFor(m => m.Channel, f => f.Commerce.Department());
        RuleFor(
            m => m.Protocol,
            f => f.PickRandom(new[] { "Irc", "Console" }));
        RuleFor(m => m.From, _ => new NickFaker().Generate());
    }
}