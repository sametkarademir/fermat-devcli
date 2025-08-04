using System.Text.Encodings.Web;
using System.Text.Json;
using Fermat.DevCli.Shared.Interfaces;

namespace Fermat.DevCli.Shared.Services;

public class ResourceAppService : IResourceAppService
{
    public string GetBasePath()
    {
        var baseDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".devcli");
        if (!Directory.Exists(baseDir))
        {
            Directory.CreateDirectory(baseDir);
        }

        return baseDir;
    }

    public string GetFilePath(string directoryName, string fileName)
    {
        var dir = Path.Combine(GetBasePath(), directoryName);
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        var filePath = Path.Combine(dir, fileName);
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
        }

        return filePath;
    }

    public async Task<T?> ReadConfiguration<T>(string directoryName, string fileName)
    {
        var path = GetFilePath(directoryName, fileName);
        var json = await File.ReadAllTextAsync(path);
        if (string.IsNullOrEmpty(json))
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(json);
    }

    public async Task WriteConfiguration(object config, string directoryName, string fileName)
    {
        var path = GetFilePath(directoryName, fileName);
        var json = JsonSerializer.Serialize(config, new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        });
        await File.WriteAllTextAsync(path, json);
    }
}