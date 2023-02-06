using Accounting.Core.Interfaces;
using Accounting.Core.Services;
using Accounting.Infrastructure.IOC;
using Microsoft.Extensions.DependencyInjection;

namespace Accounting.Core.IOC;

public static class IOC
{
    public static IServiceCollection AddCoreModule(this IServiceCollection services)
    {
        
        
        services.AddScoped<IMasterCompanyService, MasterCompanyService>();

        return services;
    }
}