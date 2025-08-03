using Fermat.DevCli.Configuration.Extensions;
using Fermat.DevCli.Configuration.Interfaces;
using Spectre.Console;

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
            AnsiConsole.MarkupLine("[bold green]Password length set to {0}[/]", Markup.Escape(value));
        }
        else
        {
            throw new ArgumentException("Invalid password length. It must be a positive integer.");
        }
    }

    public async Task<T> GetHandlerAsync<T>(string key)
    {
        try
        {
            var passwordConfiguration = await ConfigurationFileExtensions.ReadPasswordConfiguration();
            return (T)(passwordConfiguration.Length as object);
        }
        catch (Exception)
        {
            throw new InvalidOperationException($"Cannot get value for key '{key}' as type '{typeof(T).Name}' is not supported.");
        }
    }
}