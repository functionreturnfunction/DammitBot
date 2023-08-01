using DammitBot.Models;
using Dapper.FluentMap.Dommel.Mapping;

namespace DammitBot.Data.Dapper.Mappings
{
    public class UserMap : DommelEntityMap<User>
    {
        public UserMap()
        {
            Map(x => x.Id).IsKey();

            Map(x => x.CreatedAt);
            Map(x => x.UpdatedAt);
            Map(x => x.Username);
        }
    }
}
