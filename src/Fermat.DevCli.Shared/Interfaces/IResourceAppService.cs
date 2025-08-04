namespace Fermat.DevCli.Shared.Interfaces;

public interface IResourceAppService
{
    string GetBasePath();
    string GetFilePath(string directoryName, string fileName);

    Task<T?> ReadConfiguration<T>(string directoryName, string fileName);
    Task WriteConfiguration(object config, string directoryName, string fileName);
}

