using Fermat.DevCli.Configuration.Extensions;
using Fermat.DevCli.Configuration.Interfaces;

namespace Fermat.DevCli.Configuration.Services.Strategies;

public class IncludeNumbersStrategy : IConfigurationStrategy
{
    public async Task SetHandlerAsync(string key, string value)
    {
        var passwordConfiguration = await ConfigurationFileExtensions.ReadPasswordConfiguration();
        if (bool.TryParse(value, out var includeNumbers))
        {
            passwordConfiguration.IncludeNumbers = includeNumbers;
            await ConfigurationFileExtensions.WritePasswordConfiguration(passwordConfiguration);
        }
        else
        {
            throw new ArgumentException("Invalid value for IncludeNumbers. It must be 'true' or 'false'.");
        }
    }

    public async Task<string> GetHandlerAsync(string key)
    {
        try
        {
            var passwordConfiguration = await ConfigurationFileExtensions.ReadPasswordConfiguration();
            return passwordConfiguration.IncludeNumbers.ToString();
        }
        catch (Exception)
        {
            throw new InvalidOperationException($"Cannot get value for key '{key}'. Ensure the configuration file is set up correctly.");
        }
    }
}