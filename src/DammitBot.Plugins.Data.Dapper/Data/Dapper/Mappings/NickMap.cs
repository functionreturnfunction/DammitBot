using DammitBot.Data.Models;
using Dapper.FluentMap.Dommel.Mapping;

namespace DammitBot.Data.Dapper.Mappings;

/// <summary>
/// <see cref="DommelEntityMap{Nick}"/> for the <see cref="Nick"/> entity.
/// </summary>
public class NickMap : DommelEntityMap<Nick>
{
    /// <summary>
    /// Constructor for the <see cref="NickMap"/> class.
    /// </summary>
    public NickMap()
    {
        Map(x => x.Id).IsKey();

        Map(x => x.Nickname);
        Map(x => x.CreatedAt);
        Map(x => x.UpdatedAt);

        Map(x => x.UserId);
    }
}