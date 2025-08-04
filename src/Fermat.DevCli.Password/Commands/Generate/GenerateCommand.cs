using System.CommandLine;
using Fermat.DevCli.Password.Enums;
using Fermat.DevCli.Password.Interfaces;
using Fermat.DevCli.Password.Models;
using Fermat.DevCli.Shared.Abstracts;
using Fermat.DevCli.Shared.Extensions.ConsoleOutputs;

namespace Fermat.DevCli.Password.Commands.Generate;

public class GenerateCommand(IPasswordGeneratorService passwordService) : BaseCommand
{
    public override string Name => "generate";
    public override string Description => "Generates a random password";
    public override Command Configure()
    {
        var command = new Command(Name, Description);

        var lengthOption = new Option<int>(
            aliases: ["--length", "-l"],
            description: "The length of the password",
            getDefaultValue: () => 16);

        lengthOption.AddValidator(result =>
        {
            if (result.GetValueForOption(lengthOption) < 4)
            {
                result.ErrorMessage = "Password length must be at least 4 characters";
            }

            if (result.GetValueForOption(lengthOption) > 128)
            {
                result.ErrorMessage = "Password length must be at most 128 characters";
            }
        });

        var uppercaseOption = new Option<bool>(
            aliases: ["--uppercase", "-u"],
            description: "Include uppercase letters ?",
            getDefaultValue: () => true);

        var lowercaseOption = new Option<bool>(
            aliases: ["--lowercase", "-w"],
            description: "Include lowercase letters ?",
            getDefaultValue: () => true);

        var numbersOption = new Option<bool>(
            aliases: ["--numbers", "-n"],
            description: "Include numbers ?",
            getDefaultValue: () => true);

        var specialCharsOption = new Option<bool>(
            aliases: ["--special", "-s"],
            description: "Include special characters ?",
            getDefaultValue: () => true);

        var countOption = new Option<int>(
            aliases: ["--count", "-c"],
            description: "Number of passwords to generate",
            getDefaultValue: () => 1);

        countOption.AddValidator(result =>
        {
            if (result.GetValueForOption(countOption) < 1)
            {
                result.ErrorMessage = "Number of passwords to generate must be at least 1";
            }

            if (result.GetValueForOption(countOption) > 100)
            {
                result.ErrorMessage = "Number of passwords to generate must be at most 100";
            }
        });

        var typeOption = new Option<PasswordOptionTypes>(
            aliases: ["--type", "-t"],
            description: "Type of password to generate",
            getDefaultValue: () => PasswordOptionTypes.Default);

        command.AddOption(lengthOption);
        command.AddOption(uppercaseOption);
        command.AddOption(lowercaseOption);
        command.AddOption(numbersOption);
        command.AddOption(specialCharsOption);
        command.AddOption(countOption);
        command.AddOption(typeOption);

        command.SetHandler(async (type, length, uppercase, lowercase, numbers, special, count) =>
        {
            var options = new PasswordOptions
            {
                Type = type,
                Length = length,
                IncludeUppercase = uppercase,
                IncludeLowercase = lowercase,
                IncludeNumbers = numbers,
                IncludeSpecialCharacters = special
            };

            ConsoleOutputExtensions.PrintHeader("Generating Passwords");

            await ConsoleOutputExtensions.ShowProgressAsync(async (ctx) =>
            {
                var task = ctx.AddTask("Generating...", maxValue: count);

                for (var i = 0; i < count; i++)
                {
                    var password = await passwordService.GeneratePassword(options);
                    ConsoleOutputExtensions.PrintInfo($"Generated Password {i + 1}: {password}");
                    task.Increment(1);

                    await Task.Delay(100);
                }
            });
        }, typeOption, lengthOption, uppercaseOption, lowercaseOption, numbersOption, specialCharsOption, countOption);

        return command;
    }
}