using Accounting.Infrastructure.Repositories;
using Accounting.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Accounting.Infrastructure.IOC;

public static class IOC
{
    public static IServiceCollection AddSQLModule(this IServiceCollection services)
    {
        services.AddScoped<IMasterCompanyRepository, MasterCompanyRepository>();

        return services;
    }
}