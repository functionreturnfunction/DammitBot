using DammitBot.Data.Models;
using FluentNHibernate.Mapping;

namespace DammitBot.Data.NHibernate.Mappings
{
    public class ReminderMap : ClassMap<Reminder>
    {
        public ReminderMap()
        {
            Id(x => x.Id);

            Map(x => x.Text).Not.Nullable();
            Map(x => x.RemindAt).Not.Nullable();
            Map(x => x.RemindedAt).Nullable();
            Map(x => x.CreatedAt).Not.Nullable();
            Map(x => x.UpdatedAt).Nullable();

            References(x => x.From).Not.Nullable();
            References(x => x.To).Not.Nullable();
        }
    }
}
