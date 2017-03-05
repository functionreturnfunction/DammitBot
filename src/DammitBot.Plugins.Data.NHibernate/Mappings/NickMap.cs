using DammitBot.Data.Models;
using FluentNHibernate.Mapping;

namespace DammitBot.Data.NHibernate.Mappings
{
    public class NickMap : ClassMap<Nick>
    {
        public NickMap()
        {
            Id(x => x.Id);
            Map(x => x.Nickname).Not.Nullable();
            Map(x => x.CreatedAt).Not.Nullable();
            Map(x => x.UpdatedAt).Nullable();
            References(x => x.User);
        }
    }
}