namespace TeamCitySharper.Connection
{
    public interface ITeamCityCallerFactory
    {
         ITeamCityCaller Build(string hostName, bool useSsl = false);
    }
}