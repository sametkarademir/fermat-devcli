using System.CommandLine;
using Fermat.DevCli.Password.Commands.Generate;
using Fermat.DevCli.Shared.Abstracts;

namespace Fermat.DevCli.Password.Commands;

public class PasswordCommand(GenerateCommand generateCommand) : BaseCommand
{
    public override string Name => "pswd";
    public override string Description => "Password generator and length checker";
    public override Command Configure()
    {
        var command = new Command(Name, Description);

        command.AddCommand(generateCommand.Configure());

        return command;
    }
}