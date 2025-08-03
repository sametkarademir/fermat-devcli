using Fermat.DevCli.Configuration.Extensions;
using Fermat.DevCli.Configuration.Interfaces;

namespace Fermat.DevCli.Configuration.Services.Strategies;

public class UppercaseCharsStrategy : IConfigurationStrategy
{
    public async Task SetHandlerAsync(string key, string value)
    {
        var passwordConfiguration = await ConfigurationFileExtensions.ReadPasswordConfiguration();
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Uppercase characters cannot be empty.");
        }

        passwordConfiguration.UppercaseChars = value;
        await ConfigurationFileExtensions.WritePasswordConfiguration(passwordConfiguration);
    }

    public async Task<string> GetHandlerAsync(string key)
    {
        try
        {
            var passwordConfiguration = await ConfigurationFileExtensions.ReadPasswordConfiguration();
            return passwordConfiguration.UppercaseChars.ToString();
        }
        catch (Exception)
        {
            throw new InvalidOperationException($"Cannot get value for key '{key}'. Ensure the configuration file is set up correctly.");
        }
    }
}