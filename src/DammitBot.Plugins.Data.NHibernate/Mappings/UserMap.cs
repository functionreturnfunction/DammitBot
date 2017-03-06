using DammitBot.Data.Models;
using FluentNHibernate.Mapping;

namespace DammitBot.Data.NHibernate.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);

            Map(x => x.CreatedAt).Not.Nullable();
            Map(x => x.UpdatedAt).Nullable();
            Map(x => x.Username).Not.Nullable();
        }
    }
}
