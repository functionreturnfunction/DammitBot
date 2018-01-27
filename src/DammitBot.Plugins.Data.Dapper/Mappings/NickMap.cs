using DammitBot.Data.Models;
using Dapper.FluentMap.Dommel.Mapping;

namespace DammitBot.Data.Dapper.Mappings
{
    public class NickMap : DommelEntityMap<Nick>
    {
        public NickMap()
        {
            Map(x => x.Id).IsKey();

            Map(x => x.Nickname);
            Map(x => x.CreatedAt);
            Map(x => x.UpdatedAt);

            Map(x => x.User);
        }
    }
}
