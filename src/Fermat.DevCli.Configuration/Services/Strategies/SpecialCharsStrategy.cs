using Fermat.DevCli.Configuration.Extensions;
using Fermat.DevCli.Configuration.Interfaces;
using Fermat.DevCli.Shared.Extensions.ConsoleOutputs;
using Spectre.Console;

namespace Fermat.DevCli.Configuration.Services.Strategies;

public class SpecialCharsStrategy : IConfigurationStrategy
{
    public async Task SetHandlerAsync(string key, string value)
    {
        var passwordConfiguration = await ConfigurationFileExtensions.ReadPasswordConfiguration();
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("SpecialChars cannot be null or empty.");
        }

        passwordConfiguration.SpecialChars = value;
        await ConfigurationFileExtensions.WritePasswordConfiguration(passwordConfiguration);
        ConsoleOutputExtensions.PrintSuccess($"SpecialChars set to {Markup.Escape(value)}");
    }

    public async Task<string> GetHandlerAsync(string key)
    {
        try
        {
            var passwordConfiguration = await ConfigurationFileExtensions.ReadPasswordConfiguration();
            return passwordConfiguration.SpecialChars.ToString();
        }
        catch (Exception)
        {
            throw new InvalidOperationException($"Cannot get value for key '{key}'. Ensure the configuration file is set up correctly.");
        }
    }
}