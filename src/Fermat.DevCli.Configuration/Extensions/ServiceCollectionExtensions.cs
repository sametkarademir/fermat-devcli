using System.CommandLine;
using Fermat.DevCli.Configuration.Commands;
using Fermat.DevCli.Configuration.Commands.Get;
using Fermat.DevCli.Configuration.Commands.GetList;
using Fermat.DevCli.Configuration.Commands.Set;
using Microsoft.Extensions.DependencyInjection;

namespace Fermat.DevCli.Configuration.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigurationCommandServices(this IServiceCollection services)
    {
        //Commands
        services.AddSingleton<ConfigurationCommand>();
        services.AddSingleton<SetCommand>();
        services.AddSingleton<GetCommand>();
        services.AddSingleton<GetListCommand>();

        return services;
    }

    public static RootCommand AddConfigurationCommand(this RootCommand rootCommand, IServiceProvider serviceProvider)
    {
        var configCommand = serviceProvider.GetRequiredService<ConfigurationCommand>();
        rootCommand.AddCommand(configCommand.Configure());
        return rootCommand;
    }
}