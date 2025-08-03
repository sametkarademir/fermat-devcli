using Fermat.DevCli.Configuration.Extensions;
using Fermat.DevCli.Configuration.Interfaces;
using Spectre.Console;

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
        AnsiConsole.MarkupLine("[bold green]Uppercase characters set to {0}[/]", Markup.Escape(value));
    }

    public async Task<T> GetHandlerAsync<T>(string key)
    {
        try
        {
            var passwordConfiguration = await ConfigurationFileExtensions.ReadPasswordConfiguration();
            return (T)(passwordConfiguration.UppercaseChars as object);
        }
        catch (Exception)
        {
            throw new InvalidOperationException($"Cannot get value for key '{key}' as type '{typeof(T).Name}' is not supported.");
        }
    }
}