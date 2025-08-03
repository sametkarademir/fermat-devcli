namespace Fermat.DevCli.Configuration.Interfaces;

public interface IConfigurationStrategy
{
    Task SetHandlerAsync(string key, string value);
    Task<string> GetHandlerAsync(string key);
}