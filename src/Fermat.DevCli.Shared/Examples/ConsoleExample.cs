using Fermat.DevCli.Shared.Extensions;
using Fermat.DevCli.Shared.Extensions.ConsoleOutputs;
using Spectre.Console;

namespace Fermat.DevCli.Shared.Examples;

public static class ConsoleExample
{
    /// <summary>
    /// Demonstrates various console output methods using Spectre.Console
    /// </summary>
    public static void DemonstrateConsoleOutput()
    {
        // Print a header
        ConsoleOutputExtensions.PrintHeader("Fermat Dev CLI Console Demo");

        // Print different types of messages
        ConsoleOutputExtensions.PrintInfo("This is an informational message");
        ConsoleOutputExtensions.PrintSuccess("Operation completed successfully!");
        ConsoleOutputExtensions.PrintWarning("This is a warning message");
        ConsoleOutputExtensions.PrintError("This is an error message");

        // Create and display a table
        var table = ConsoleOutputExtensions.CreateTable("Name", "Status", "Version");
        table.AddRow("Fermat CLI", "Active", "1.0.0");
        table.AddRow("Spectre.Console", "Loaded", "0.48.0");
        table.AddRow("System.CommandLine", "Ready", "2.0.0");

        ConsoleOutputExtensions.PrintTable(table);

        // Show progress example
        ConsoleOutputExtensions.ShowProgress("Processing data...", ctx =>
        {
            var task = ctx.AddTask("Loading modules");
            task.Increment(50);
            Thread.Sleep(500);
            task.Increment(50);
        });

        // Show status example
        ConsoleOutputExtensions.ShowStatus("Initializing...", () =>
        {
            Thread.Sleep(1000);
        });

        ConsoleOutputExtensions.PrintSuccess("Demo completed!");
    }
}