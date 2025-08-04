using System.CommandLine;
using Fermat.DevCli.Password.Commands;
using Fermat.DevCli.Password.Commands.Generate;
using Fermat.DevCli.Password.Interfaces;
using Fermat.DevCli.Password.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fermat.DevCli.Password.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPasswordCommandServices(this IServiceCollection services)
    {
        //Services
        services.AddSingleton<IPasswordGeneratorService, PasswordGeneratorService>();
        services.AddSingleton<IRandomProvider, RandomProvider>();

        //Commands
        services.AddSingleton<PasswordCommand>();
        services.AddSingleton<GenerateCommand>();

        return services;
    }

    public static RootCommand AddPasswordCommand(this RootCommand rootCommand, IServiceProvider serviceProvider)
    {
        var passwordCommand = serviceProvider.GetRequiredService<PasswordCommand>();
        rootCommand.AddCommand(passwordCommand.Configure());
        return rootCommand;
    }
}