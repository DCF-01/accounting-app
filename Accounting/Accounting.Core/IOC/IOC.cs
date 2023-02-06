using Accounting.Core.Interfaces;
using Accounting.Core.Services;
using Accounting.Core.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace Accounting.Core.IOC;

public static class IOC
{
    public static IServiceCollection AddCoreModule(this IServiceCollection services)
    {
        services.AddScoped<IMasterCompanyService, MasterCompanyService>();
        services.AddScoped<IUserInformation, IUserInformation>();
        
        services.ConfigureAutoMapper();

        return services;
    }

    private static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { cfg.AddProfile<MasterUserProfile>(); });
    }
}