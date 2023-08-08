using DammitBot.Data.Models;
using Dapper.FluentMap.Dommel.Mapping;

namespace DammitBot.Data.Dapper.Mappings;

/// <summary>
/// <see cref="DommelEntityMap{TEntity}"/> for the <see cref="User"/> entity.
/// </summary>
public class UserMap : DommelEntityMap<User>
{
    /// <summary>
    /// Constructor for the <see cref="UserMap"/> class.
    /// </summary>
    public UserMap()
    {
        Map(x => x.Id).IsKey();

        Map(x => x.CreatedAt);
        Map(x => x.UpdatedAt);
        Map(x => x.Username);
    }
}