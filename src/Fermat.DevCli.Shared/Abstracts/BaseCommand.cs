using System.CommandLine;

namespace Fermat.DevCli.Shared.Abstracts;

public abstract class BaseCommand
{
    public abstract string Name { get; }
    public abstract string Description { get; }

    public abstract Command Configure();
}