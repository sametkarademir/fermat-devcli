using System.CommandLine;
using Fermat.DevCli.Configuration.Extensions;
using Fermat.DevCli.Shared.Extensions.ConsoleOutputs;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace Fermat.DevCli.App;

public static class Program
{
    public static async Task Main(string[] args)
    {
        try
        {
            var services =
                new ServiceCollection()
                    .AddConfigurationCommandServices()
                    .BuildServiceProvider();

            var rootCommand = new RootCommand("Fermat Development CLI - A powerful development tool");
            ConsoleOutputExtensions.PrintHeader("Welcome to Fermat Dev CLI!");
            ConsoleOutputExtensions.PrintInfo("This CLI tool provides various commands to assist with development tasks.");
            AnsiConsole.WriteLine();

            // Add sample commands
            rootCommand.AddConfigurationCommand(services);
            await rootCommand.InvokeAsync(args);
        }
        catch (Exception e)
        {
            ConsoleOutputExtensions.PrintError("An error occurred while executing the command.");
            ConsoleOutputExtensions.PrintError(e.Message);
        }
    }
}