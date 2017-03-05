using DammitBot.Data.NHibernate.Library;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;

namespace DammitBot.Data.NHibernate.Mappings
{
    public abstract class NHibernateMappingConfigurationBase<TAssemblyOf> : IMappingConfiguration
    {
        #region Exposed Methods

        public void Configure(MappingConfiguration configuration)
        {
            configuration.FluentMappings.AddFromAssemblyOf<TAssemblyOf>()
                .Conventions.Add(
                    Table.Is(x => Inflector.Inflector.Pluralize(x.EntityType.Name)),
                    PrimaryKey.Name.Is(_ => "Id"),
                    ForeignKey.EndsWith("Id"));
        }

        #endregion
    }
}