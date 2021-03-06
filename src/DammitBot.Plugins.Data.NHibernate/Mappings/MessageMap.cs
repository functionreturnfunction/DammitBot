﻿using DammitBot.Data.Models;
using FluentNHibernate.Mapping;

namespace DammitBot.Data.NHibernate.Mappings
{
    public class MessageMap : ClassMap<Message>
    {
        public MessageMap()
        {
            Id(x => x.Id);
            Map(x => x.Text).Not.Nullable();
            Map(x => x.Protocol);
            Map(x => x.Channel);
            Map(x => x.CreatedAt).Not.Nullable();
            Map(x => x.UpdatedAt).Nullable();
            References(x => x.From).Not.Nullable();
        }
    }
}