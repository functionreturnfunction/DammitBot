using DammitBot.Data.Models;
using Dapper.FluentMap.Dommel.Mapping;

namespace DammitBot.Data.Dapper.Mappings;

/// <summary>
/// <see cref="DommelEntityMap{Message}"/> for the <see cref="Message"/> entity.
/// </summary>
public class MessageMap : DommelEntityMap<Message>
{
    /// <summary>
    /// Constructor for the <see cref="MessageMap"/> class.
    /// </summary>
    public MessageMap()
    {
        Map(p => p.Id).IsKey();

        Map(x => x.Text);
        Map(x => x.Protocol);
        Map(x => x.Channel);
        Map(x => x.CreatedAt);
        Map(x => x.UpdatedAt);

        Map(x => x.From.Id);
    }
}