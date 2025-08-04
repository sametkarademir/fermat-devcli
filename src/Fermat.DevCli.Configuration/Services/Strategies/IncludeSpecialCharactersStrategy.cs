using Fermat.DevCli.Configuration.Constans;
using Fermat.DevCli.Configuration.Extensions;
using Fermat.DevCli.Configuration.Interfaces;
using Fermat.DevCli.Configuration.Models;
using Fermat.DevCli.Shared.Interfaces;

namespace Fermat.DevCli.Configuration.Services.Strategies;

public class IncludeSpecialCharactersStrategy(IResourceAppService resourceAppService) : IConfigurationStrategy
{
    public async Task SetHandlerAsync(string key, string value)
    {
        var passwordConfiguration = await resourceAppService.ReadConfiguration<PasswordConfiguration>
        (
            ConfigurationConsts.DirectoryPath,
            ConfigurationConsts.PasswordConfigFileName
        );

        if (passwordConfiguration == null)
        {
            throw new InvalidOperationException("Password configuration is not set up correctly.");
        }

        if (bool.TryParse(value, out var includeSpecialChars))
        {
            passwordConfiguration.IncludeSpecialCharacters = includeSpecialChars;
            await resourceAppService.WriteConfiguration
            (
                passwordConfiguration,
                ConfigurationConsts.DirectoryPath,
                ConfigurationConsts.PasswordConfigFileName
            );
        }
        else
        {
            throw new ArgumentException("Invalid value for IncludeSpecialChars. It must be 'true' or 'false'.");
        }
    }

    public async Task<string> GetHandlerAsync(string key)
    {
        try
        {
            var passwordConfiguration = await resourceAppService.ReadConfiguration<PasswordConfiguration>
            (
                ConfigurationConsts.DirectoryPath,
                ConfigurationConsts.PasswordConfigFileName
            );

            if (passwordConfiguration == null)
            {
                throw new InvalidOperationException("Password configuration is not set up correctly.");
            }

            return passwordConfiguration.IncludeSpecialCharacters.ToString();
        }
        catch (Exception)
        {
            throw new InvalidOperationException($"Cannot get value for key '{key}'. Ensure the configuration file is set up correctly.");
        }
    }
}