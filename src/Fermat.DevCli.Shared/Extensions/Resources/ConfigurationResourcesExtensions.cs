namespace Fermat.DevCli.Shared.Extensions.Resources;

public static class ConfigurationResourcesExtensions
{
    private static string GetBasePath()
    {
        var baseDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".devcli");
        if (!Directory.Exists(baseDir))
        {
            Directory.CreateDirectory(baseDir);
        }
        return baseDir;
    }

    public static string GetFilePath(string directoryName, string fileName)
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


}