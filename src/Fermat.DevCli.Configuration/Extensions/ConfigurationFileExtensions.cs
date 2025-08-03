using System.Text.Encodings.Web;
using System.Text.Json;
using Fermat.DevCli.Configuration.Constans;
using Fermat.DevCli.Configuration.Models;
using Fermat.DevCli.Shared.Extensions.Resources;

namespace Fermat.DevCli.Configuration.Extensions;

public static class ConfigurationFileExtensions
{
    public static async Task<string> ReadAllConfiguration()
    {
        var path = ConfigurationResourcesExtensions.GetFilePath(ConfigurationConsts.DirectoryPath, ConfigurationConsts.PasswordConfigFileName);
        if (!File.Exists(path))
        {
            return string.Empty;
        }

        return await File.ReadAllTextAsync(path);
    }

    public static async Task<PasswordConfiguration> ReadPasswordConfiguration()
    {
        var path = ConfigurationResourcesExtensions.GetFilePath(ConfigurationConsts.DirectoryPath, ConfigurationConsts.PasswordConfigFileName);
        var json = await File.ReadAllTextAsync(path);
        if (string.IsNullOrEmpty(json))
        {
            return new PasswordConfiguration();
        }

        return JsonSerializer.Deserialize<PasswordConfiguration>(json) ?? new PasswordConfiguration();
    }

    public static async Task WritePasswordConfiguration(PasswordConfiguration config)
    {
        var path = ConfigurationResourcesExtensions.GetFilePath(ConfigurationConsts.DirectoryPath, ConfigurationConsts.PasswordConfigFileName);
        var json = JsonSerializer.Serialize(config, new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
        await File.WriteAllTextAsync(path, json);
    }
}