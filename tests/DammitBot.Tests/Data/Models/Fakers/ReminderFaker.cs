using Bogus;

namespace DammitBot.Data.Models.Fakers;

public class ReminderFaker : Faker<Reminder>
{
    public ReminderFaker()
    {
        var userFaker = new UserFaker();
        RuleFor(r => r.Text, f => f.Lorem.Text());
        RuleFor(r => r.RemindAt, f => f.Date.Future());
        RuleFor(r => r.From, _ => userFaker.Generate());
        RuleFor(r => r.To, _ => userFaker.Generate());
    }
}