using Fermat.DevCli.Configuration.Extensions;
using Fermat.DevCli.Configuration.Interfaces;

namespace Fermat.DevCli.Configuration.Services.Strategies;

public class LengthStrategy : IConfigurationStrategy
{
    public async Task SetHandlerAsync(string key, string value)
    {
        var passwordConfiguration = await ConfigurationFileExtensions.ReadPasswordConfiguration();
        if (int.TryParse(value, out var length) && length > 0)
        {
            passwordConfiguration.Length = length;
            await ConfigurationFileExtensions.WritePasswordConfiguration(passwordConfiguration);
        }
        else
        {
            throw new ArgumentException("Invalid password length. It must be a positive integer.");
        }
    }

    public async Task<string> GetHandlerAsync(string key)
    {
        try
        {
            var passwordConfiguration = await ConfigurationFileExtensions.ReadPasswordConfiguration();
            return passwordConfiguration.Length.ToString();
        }
        catch (Exception)
        {
            throw new InvalidOperationException($"Cannot get value for key '{key}'. Ensure the configuration file is set up correctly.");
        }
    }
}