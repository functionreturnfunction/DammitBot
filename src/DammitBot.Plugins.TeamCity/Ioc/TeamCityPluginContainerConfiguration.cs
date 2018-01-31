using DammitBot.Abstract;
using DammitBot.Configuration;
using DammitBot.Helpers;
using StructureMap;
using TeamCitySharper;

namespace DammitBot.Ioc
{
    public class TeamCityPluginContainerConfiguration : PluginContainerConfigurationBase
    {
        #region Exposed Methods

        public override void Configure(ConfigurationExpression e)
        {
            e.For<ITeamCityHelper>().Use<TeamCityHelper>().Singleton();
            e.For<ITeamCityClient>().Use(ctx => InitTeamCityClient(ctx));
            e.For<ITeamCityConfigurationSection>()
                .Use(ctx => ctx.GetInstance<ITeamCityConfigurationManager>().TeamCityConfigurationSection);
        }

        #endregion

        #region Private Methods

        private static ITeamCityClient InitTeamCityClient(IContext ctx)
        {
            var config = ctx.GetInstance<TeamCityConfigurationSection>();
            return new TeamCityClient(config.Host, false);
        }

        #endregion
    }
}
