using Spectre.Console;

namespace Fermat.DevCli.Shared.Extensions.ConsoleOutputs;

public static class ConsoleOutputExtensions
{
    /// <summary>
    /// Prints a success message with green color
    /// </summary>
    /// <param name="message">The message to display</param>
    public static void PrintSuccess(string message)
    {
        AnsiConsole.MarkupLine($"[green]✓ {message}[/]");
    }

    /// <summary>
    /// Prints an error message with red color
    /// </summary>
    /// <param name="message">The message to display</param>
    public static void PrintError(string message)
    {
        AnsiConsole.MarkupLine($"[red]✗ {message}[/]");
    }

    /// <summary>
    /// Prints a warning message with yellow color
    /// </summary>
    /// <param name="message">The message to display</param>
    public static void PrintWarning(string message)
    {
        AnsiConsole.MarkupLine($"[yellow]⚠ {message}[/]");
    }

    /// <summary>
    /// Prints an info message with blue color
    /// </summary>
    /// <param name="message">The message to display</param>
    public static void PrintInfo(string message)
    {
        AnsiConsole.MarkupLine($"[blue]ℹ {message}[/]");
    }

    /// <summary>
    /// Prints a header with bold formatting
    /// </summary>
    /// <param name="title">The header title</param>
    public static void PrintHeader(string title)
    {
        AnsiConsole.Write(new Rule($"[bold blue]{title}[/]").RuleStyle("blue").Centered());
    }

    /// <summary>
    /// Prints a progress bar
    /// </summary>
    /// <param name="description">Description of the operation</param>
    /// <param name="action">The action to perform with progress</param>
    public static void ShowProgress(string description, Action<ProgressContext> action)
    {
        AnsiConsole.Progress()
            .Start(ctx => action(ctx));
    }

    /// <summary>
    /// Creates a table with the specified headers
    /// </summary>
    /// <param name="headers">Table headers</param>
    /// <returns>A configured table</returns>
    public static Table CreateTable(params string[] headers)
    {
        var table = new Table();
        foreach (var header in headers)
        {
            table.AddColumn(new TableColumn(header));
        }
        table.Border = TableBorder.Rounded;
        return table;
    }

    /// <summary>
    /// Prints a table to the console
    /// </summary>
    /// <param name="table">The table to display</param>
    public static void PrintTable(Table table)
    {
        AnsiConsole.Write(table);
    }

    /// <summary>
    /// Prints a simple message with default styling
    /// </summary>
    /// <param name="message">The message to display</param>
    public static void PrintMessage(string message)
    {
        AnsiConsole.WriteLine(message);
    }

    /// <summary>
    /// Prints a message with custom color
    /// </summary>
    /// <param name="message">The message to display</param>
    /// <param name="color">The color to use</param>
    public static void PrintMessage(string message, string color)
    {
        AnsiConsole.MarkupLine($"[{color}]{message}[/]");
    }

    /// <summary>
    /// Creates a status display for long-running operations
    /// </summary>
    /// <param name="description">Description of the operation</param>
    /// <param name="action">The action to perform</param>
    public static void ShowStatus(string description, Action action)
    {
        AnsiConsole.Status()
            .Start(description, ctx => action());
    }
}