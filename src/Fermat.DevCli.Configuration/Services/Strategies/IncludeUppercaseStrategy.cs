using Fermat.DevCli.Configuration.Extensions;
using Fermat.DevCli.Configuration.Interfaces;
using Spectre.Console;

namespace Fermat.DevCli.Configuration.Services.Strategies;

public class IncludeUppercaseStrategy : IConfigurationStrategy
{
    public async Task SetHandlerAsync(string key, string value)
    {
        var passwordConfiguration = await ConfigurationFileExtensions.ReadPasswordConfiguration();
        if (bool.TryParse(value, out var includeUppercase))
        {
            passwordConfiguration.IncludeUppercase = includeUppercase;
            await ConfigurationFileExtensions.WritePasswordConfiguration(passwordConfiguration);
            AnsiConsole.MarkupLine("[bold green]IncludeUppercase set to {0}[/]", Markup.Escape(value));
        }
        else
        {
            throw new ArgumentException("Invalid value for IncludeUppercase. It must be 'true' or 'false'.");
        }
    }

    public async Task<T> GetHandlerAsync<T>(string key)
    {
        try
        {
            var passwordConfiguration = await ConfigurationFileExtensions.ReadPasswordConfiguration();
            return (T)(passwordConfiguration.IncludeUppercase as object);
        }
        catch (Exception)
        {
            throw new InvalidOperationException($"Cannot get value for key '{key}' as type '{typeof(T).Name}' is not supported.");
        }
    }
}