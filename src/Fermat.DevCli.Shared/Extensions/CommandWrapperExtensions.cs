using System.CommandLine;
using System.CommandLine.Invocation;
using Fermat.DevCli.Shared.Extensions.ConsoleOutputs;
using Spectre.Console;

namespace Fermat.DevCli.Shared.Extensions;

public static class CommandWrapperExtensions
{
    /// <summary>
    /// Wraps a command with standard header and footer
    /// </summary>
    /// <param name="command">The command to wrap</param>
    /// <param name="commandName">Name of the command for display</param>
    /// <returns>The wrapped command</returns>
    public static Command WithStandardWrapper(this Command command, string commandName)
    {
        var originalHandler = command.Handler;

        command.SetHandler(async (context) =>
        {
            try
            {
                // Print header
                PrintCommandHeader(commandName);

                // Execute original handler
                if (originalHandler != null)
                {
                    await originalHandler.InvokeAsync(context);
                }

                // Print footer
                PrintCommandFooter(commandName);
            }
            catch (Exception ex)
            {
                PrintCommandError(commandName, ex);
                context.ExitCode = 1;
            }
        });

        return command;
    }

    /// <summary>
    /// Wraps a command with standard header and footer (async version)
    /// </summary>
    /// <param name="command">The command to wrap</param>
    /// <param name="commandName">Name of the command for display</param>
    /// <param name="handler">The async handler function</param>
    /// <returns>The wrapped command</returns>
    public static Command WithStandardWrapper(this Command command, string commandName, Func<InvocationContext, Task> handler)
    {
        command.SetHandler(async (context) =>
        {
            try
            {
                // Print header
                PrintCommandHeader(commandName);

                // Execute handler
                await handler(context);

                // Print footer
                PrintCommandFooter(commandName);
            }
            catch (Exception ex)
            {
                PrintCommandError(commandName, ex);
                context.ExitCode = 1;
            }
        });

        return command;
    }

    /// <summary>
    /// Wraps a command with standard header and footer (sync version)
    /// </summary>
    /// <param name="command">The command to wrap</param>
    /// <param name="commandName">Name of the command for display</param>
    /// <param name="handler">The sync handler function</param>
    /// <returns>The wrapped command</returns>
    public static Command WithStandardWrapper(this Command command, string commandName, Action<InvocationContext> handler)
    {
        command.SetHandler(async (context) =>
        {
            try
            {
                // Print header
                PrintCommandHeader(commandName);

                // Execute handler
                handler(context);

                // Print footer
                PrintCommandFooter(commandName);
            }
            catch (Exception ex)
            {
                PrintCommandError(commandName, ex);
                context.ExitCode = 1;
            }
        });

        return command;
    }

    private static void PrintCommandHeader(string commandName)
    {
        AnsiConsole.Clear();
        ConsoleOutputExtensions.PrintHeader($"Fermat Dev CLI - {commandName}");
        ConsoleOutputExtensions.PrintInfo($"Starting {commandName} command...");
        AnsiConsole.WriteLine();
    }

    private static void PrintCommandFooter(string commandName)
    {
        AnsiConsole.WriteLine();
        ConsoleOutputExtensions.PrintSuccess($"{commandName} command completed successfully!");

        // Print execution time if needed
        var rule = new Rule("[dim]Command completed[/]")
            .RuleStyle("dim")
            .Centered();
        AnsiConsole.Write(rule);
    }

    private static void PrintCommandError(string commandName, Exception ex)
    {
        AnsiConsole.WriteLine();
        ConsoleOutputExtensions.PrintError($"{commandName} command failed!");
        ConsoleOutputExtensions.PrintError($"Error: {ex.Message}");

        if (ex.InnerException != null)
        {
            ConsoleOutputExtensions.PrintError($"Inner Error: {ex.InnerException.Message}");
        }

        var rule = new Rule("[dim red]Command failed[/]")
            .RuleStyle("red")
            .Centered();
        AnsiConsole.Write(rule);
    }
}