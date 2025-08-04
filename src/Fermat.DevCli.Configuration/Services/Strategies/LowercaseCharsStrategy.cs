using Fermat.DevCli.Configuration.Constans;
using Fermat.DevCli.Configuration.Interfaces;
using Fermat.DevCli.Configuration.Models;
using Fermat.DevCli.Shared.Interfaces;

namespace Fermat.DevCli.Configuration.Services.Strategies;

public class LowercaseCharsStrategy(IResourceAppService resourceAppService) : IConfigurationStrategy
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

        passwordConfiguration.LowercaseChars = value;
        await resourceAppService.WriteConfiguration
        (
            passwordConfiguration,
            ConfigurationConsts.DirectoryPath,
            ConfigurationConsts.PasswordConfigFileName
        );
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

            return passwordConfiguration.LowercaseChars.ToString();
        }
        catch (Exception)
        {
            throw new InvalidOperationException($"Cannot get value for key '{key}'. Ensure the configuration file is set up correctly.");
        }
    }
}