using FluentNHibernate.Cfg;

namespace DammitBot.Data.NHibernate.Library
{
    public interface IMappingConfiguration
    {
        void Configure(MappingConfiguration configuration);
    }
}