namespace DammitBot.Configuration;

public interface IDataConfigurationProvider : IConfigurationProvider
{
    string ConnectionString { get; }
}