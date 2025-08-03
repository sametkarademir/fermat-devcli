using System.CommandLine;
using Fermat.DevCli.Shared.Extensions;
using Fermat.DevCli.Shared.Extensions.ConsoleOutputs;
using Spectre.Console;

namespace Fermat.DevCli.Shared.Examples;

public static class SampleCommands
{
    /// <summary>
    /// Creates a sample info command
    /// </summary>
    /// <returns>The info command</returns>
    public static Command CreateInfoCommand()
    {
        var command = new Command("info", "Display system information")
            .WithStandardWrapper("Info", async (context) =>
            {
                // Simulate some work
                ConsoleOutputExtensions.ShowStatus("Gathering system information...", () =>
                {
                    Thread.Sleep(1000);
                });

                // Display system info
                var table = ConsoleOutputExtensions.CreateTable("Property", "Value");
                table.AddRow("OS", Environment.OSVersion.ToString());
                table.AddRow("Framework", Environment.Version.ToString());
                table.AddRow("Machine Name", Environment.MachineName);
                table.AddRow("User Name", Environment.UserName);
                table.AddRow("Working Directory", Environment.CurrentDirectory);

                ConsoleOutputExtensions.PrintTable(table);
            });

        return command;
    }

    /// <summary>
    /// Creates a sample test command
    /// </summary>
    /// <returns>The test command</returns>
    public static Command CreateTestCommand()
    {
        var command = new Command("test", "Run tests")
            .WithStandardWrapper("Test", async (context) =>
            {
                // Simulate test execution
                ConsoleOutputExtensions.ShowProgress("Running tests...", ctx =>
                {
                    var testTask = ctx.AddTask("Unit Tests");
                    testTask.Increment(30);
                    Thread.Sleep(500);
                    testTask.Increment(70);

                    var integrationTask = ctx.AddTask("Integration Tests");
                    integrationTask.Increment(50);
                    Thread.Sleep(300);
                    integrationTask.Increment(50);
                });

                ConsoleOutputExtensions.PrintSuccess("All tests passed!");
            });

        return command;
    }

    /// <summary>
    /// Creates a sample build command
    /// </summary>
    /// <returns>The build command</returns>
    public static Command CreateBuildCommand()
    {
        var command = new Command("build", "Build the project")
            .WithStandardWrapper("Build", async (context) =>
            {
                // Simulate build process
                ConsoleOutputExtensions.ShowStatus("Building project...", () =>
                {
                    Thread.Sleep(2000);
                });

                ConsoleOutputExtensions.PrintSuccess("Build completed successfully!");
                ConsoleOutputExtensions.PrintInfo("Output files generated in bin/ directory");
            });

        return command;
    }
}