using System.CommandLine;
using System.Text.Json;
using Fermat.DevCli.Configuration.Constans;
using Fermat.DevCli.Configuration.Extensions;
using Fermat.DevCli.Shared.Abstracts;
using Fermat.DevCli.Shared.Extensions.ConsoleOutputs;
using Spectre.Console;

namespace Fermat.DevCli.Configuration.Commands.GetList;

public class GetListCommand : BaseCommand
{
    public override string Name => "get-list";
    public override string Description => "List all available configuration options";

    public override Command Configure()
    {
        var command = new Command(Name, Description);

        command.SetHandler(async () =>
        {
            var passwordConfiguration = await ConfigurationFileExtensions.ReadPasswordConfiguration();

            var table = ConsoleOutputExtensions.CreateTable("Key", "Value");

            // Password Configuration
            table.AddRow(ConfigurationConsts.PasswordLength, passwordConfiguration.Length.ToString());
            table.AddRow(ConfigurationConsts.PasswordIncludeUppercase, passwordConfiguration.IncludeUppercase.ToString());
            table.AddRow(ConfigurationConsts.PasswordUppercaseChars, Markup.Escape(passwordConfiguration.UppercaseChars));
            table.AddRow(ConfigurationConsts.PasswordIncludeLowercase, passwordConfiguration.IncludeLowercase.ToString());
            table.AddRow(ConfigurationConsts.PasswordLowercaseChars, Markup.Escape(passwordConfiguration.LowercaseChars));
            table.AddRow(ConfigurationConsts.PasswordIncludeNumbers, passwordConfiguration.IncludeNumbers.ToString());
            table.AddRow(ConfigurationConsts.PasswordNumberChars, Markup.Escape(passwordConfiguration.NumberChars));
            table.AddRow(ConfigurationConsts.PasswordIncludeSpecialCharacters, passwordConfiguration.IncludeSpecialCharacters.ToString());
            table.AddRow(ConfigurationConsts.PasswordSpecialChars, Markup.Escape(passwordConfiguration.SpecialChars));
            table.AddRow(ConfigurationConsts.PasswordExcludeChars, Markup.Escape(passwordConfiguration.ExcludeChars));


            ConsoleOutputExtensions.PrintTable(table);
        });

        return command;
    }
}