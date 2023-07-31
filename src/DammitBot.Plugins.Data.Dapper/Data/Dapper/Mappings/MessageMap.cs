using DammitBot.Data.Models;
using Dapper.FluentMap.Dommel.Mapping;

namespace DammitBot.Data.Dapper.Mappings
{
    public class MessageMap : DommelEntityMap<Message>
    {
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
}
