using DammitBot.Data.Models;
using Dapper.FluentMap.Dommel.Mapping;

namespace DammitBot.Data.Dapper.Mappings
{
    public class ReminderMap : DommelEntityMap<Reminder>
    {
        public ReminderMap()
        {
            Map(x => x.Id).IsKey();

            Map(x => x.Text);
            Map(x => x.RemindAt);
            Map(x => x.RemindedAt);
            Map(x => x.CreatedAt);
            Map(x => x.UpdatedAt);

            Map(x => x.From.Id);
            Map(x => x.To.Id);
        }
    }
}
