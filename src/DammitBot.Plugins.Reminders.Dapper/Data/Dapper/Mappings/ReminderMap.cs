using DammitBot.Data.Models;
using Dapper.FluentMap.Dommel.Mapping;

namespace DammitBot.Data.Dapper.Mappings;

/// <summary>
/// <see cref="DommelEntityMap{TEntity}"/> for the <see cref="Reminder"/> entity.
/// </summary>
public class ReminderMap : DommelEntityMap<Reminder>
{
    /// <summary>
    /// Constructor for the <see cref="ReminderMap"/> class.
    /// </summary>
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