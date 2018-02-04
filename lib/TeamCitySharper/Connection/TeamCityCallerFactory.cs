namespace TeamCitySharper.Connection
{
    public class TeamCityCallerFactory : ITeamCityCallerFactory
    {
        public ITeamCityCaller Build(string hostName, bool useSsl = false)
        {
            return new TeamCityCaller(hostName, useSsl);
        }
    }
}