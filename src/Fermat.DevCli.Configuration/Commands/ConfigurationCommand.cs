using System.CommandLine;
using Fermat.DevCli.Configuration.Commands.Get;
using Fermat.DevCli.Configuration.Commands.GetList;
using Fermat.DevCli.Configuration.Commands.Set;
using Fermat.DevCli.Shared.Abstracts;

namespace Fermat.DevCli.Configuration.Commands;

public class ConfigurationCommand(
    SetCommand setCommand,
    GetCommand getCommand,
    GetListCommand getListCommand
    ) : BaseCommand
{
    public override string Name => "conf";
    public override string Description => "Configuration management commands";
    public override Command Configure()
    {
        var command = new Command(Name, Description);

        command.AddCommand(setCommand.Configure());
        command.AddCommand(getCommand.Configure());
        command.AddCommand(getListCommand.Configure());

        return command;
    }
}