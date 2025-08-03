using System.CommandLine;
using Fermat.DevCli.Configuration.Constans;
using Fermat.DevCli.Configuration.Services;
using Fermat.DevCli.Shared.Abstracts;
using Spectre.Console;

namespace Fermat.DevCli.Configuration.Commands.Get;

public class GetCommand : BaseCommand
{
    public override string Name => "get";
    public override string Description => "Get a configuration value";

    public override Command Configure()
    {
        var command = new Command(Name, Description);

        var keyArgument = new Argument<string>(
            name: "key",
            description: "Configuration key to set"
        )
        {
            Arity = ArgumentArity.ExactlyOne,
        };
        keyArgument.AddValidator(validate =>
        {
            if (string.IsNullOrWhiteSpace(validate.GetValueForArgument(keyArgument)))
            {
                validate.ErrorMessage = "Configuration key cannot be empty";
            }

            if (validate.GetValueForArgument(keyArgument).Length > 256)
            {
                validate.ErrorMessage = "Configuration key cannot exceed 256 characters";
            }

            if (!ConfigurationConsts.IsValidKey(validate.GetValueForArgument(keyArgument)))
            {
                validate.ErrorMessage = "Configuration key not supported. Supported keys are: " + string.Join(", ", ConfigurationConsts.AllKeys);
            }
        });

        command.AddArgument(keyArgument);

        command.SetHandler(async (key) =>
        {

            var builder = new ConfigurationBuilder();
            var result = await builder.GetHandlerAsync<string>(key);
            if (string.IsNullOrWhiteSpace(result))
            {

                AnsiConsole.MarkupLine("[bold red]Configuration key '{0}' not found.[/]", Markup.Escape(key));
            }
            else
            {
                AnsiConsole.MarkupLine("[bold green]Configuration key '{0}' has value: {1}[/]", Markup.Escape(key), Markup.Escape(result));
            }

        }, keyArgument);

        return command;
    }

}