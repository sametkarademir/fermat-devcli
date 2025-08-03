using System.CommandLine;
using Fermat.DevCli.Configuration.Constans;
using Fermat.DevCli.Configuration.Services;
using Fermat.DevCli.Shared.Abstracts;
using Fermat.DevCli.Shared.Extensions.ConsoleOutputs;

namespace Fermat.DevCli.Configuration.Commands.Set;

public class SetCommand : BaseCommand
{
    public override string Name => "set";
    public override string Description => "Set a configuration value";
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

        var valueArgument = new Argument<string>(
            name: "value",
            description: "Value to set for the configuration key"
        )
        {
            Arity = ArgumentArity.ExactlyOne,
        };
        valueArgument.AddValidator(validate =>
        {
            if (string.IsNullOrWhiteSpace(validate.GetValueForArgument(valueArgument)))
            {
                validate.ErrorMessage = "Configuration value cannot be empty";
            }

            if (validate.GetValueForArgument(valueArgument).Length > 1024)
            {
                validate.ErrorMessage = "Configuration value cannot exceed 1024 characters";
            }
        });

        command.AddArgument(keyArgument);
        command.AddArgument(valueArgument);

        command.SetHandler(async (key, value) =>
        {

            var builder = new ConfigurationBuilder();
            await builder.SetHandlerAsync(key, value);
            ConsoleOutputExtensions.PrintSuccess($"Configuration key '{key}' set to: {value}");

        }, keyArgument, valueArgument);

        return command;
    }
}