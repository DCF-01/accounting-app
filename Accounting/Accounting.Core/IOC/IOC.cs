using Accounting.Core.Interfaces;
using Accounting.Core.Services;
using Accounting.Core.Mappings;
using Accounting.Core.Models;
using Accounting.Infrastructure.Repositories;
using Accounting.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Accounting.Core.IOC;

public static class IOC
{
    public static IServiceCollection AddCoreModule(this IServiceCollection services)
    {
        services.AddScoped<IMasterCompanyService, MasterCompanyService>();
        services.AddScoped<IBankAccountRepository, BankAccountRepository>();
        services.AddScoped<IBankRepository, BankRepository>();
        services.AddScoped<IUserInformation, UserInformation>();
        
        services.ConfigureAutoMapper();

        return services;
    }

    private static void ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<MasterCompanyProfile>();
            cfg.AddProfile<BankProfile>();
            cfg.AddProfile<BankAccountProfile>();
        });
    }
}