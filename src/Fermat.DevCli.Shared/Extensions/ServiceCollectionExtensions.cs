using Fermat.DevCli.Shared.Interfaces;
using Fermat.DevCli.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fermat.DevCli.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSharedCommandServices(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IResourceAppService, ResourceAppService>();

        return services;
    }
}