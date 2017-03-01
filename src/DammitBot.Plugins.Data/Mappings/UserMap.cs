using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DammitBot.Data.Models;
using FluentNHibernate.Mapping;

namespace DammitBot.Data.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id();
            Map(x => x.Username).Not.Nullable();
        }
    }

    public class NickMap : ClassMap<Nick>
    {
        public NickMap()
        {
            Id();
            Map(x => x.Nickname);
            References(x => x.User);
        }
    }

    public class MessageMap : ClassMap<Message>
    {
        public MessageMap()
        {
            Id();
            Map(x => x.Text);
            References(x => x.From);
        }
    }
}
