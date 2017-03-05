using FluentNHibernate.Cfg;

namespace DammitBot.Data.NHibernate.Library
{
    public interface IMappingConfigurationService
    {
        void Configure(MappingConfiguration mappingConfiguration);
    }
}