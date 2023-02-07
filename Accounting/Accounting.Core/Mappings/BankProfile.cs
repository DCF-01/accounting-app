using Accounting.Core.Requests;
using Accounting.Infrastructure.Models;
using AutoMapper;

namespace Accounting.Core.Mappings;

public class BankProfile : Profile
{
    public BankProfile()
    {
        CreateMap<Bank, BankGet>();
        CreateMap<BankGet, Bank>();

        CreateMap<BankPost, Bank>()
            .ForMember(x => x.BankAccounts,
                cfg =>
                    cfg.MapFrom(x => x.BankAccountIds.Select(id =>
                        new BankAccount
                        {
                            BankAccountId = id
                        })))
            .ForMember(x => x.Name, cfg => cfg.MapFrom(x => x.Name))
            .ForMember(x => x.SWIFT, cfg => cfg.MapFrom(x => x.SWIFT));

        
        CreateMap<BankPut, Bank>()
            .ForMember(x => x.BankAccounts,
                cfg =>
                    cfg.MapFrom(x => x.BankAccountIds.Select(id =>
                        new BankAccount
                        {
                            BankAccountId = id
                        })))
            .ForMember(x => x.Name, cfg => cfg.MapFrom(x => x.Name))
            .ForMember(x => x.SWIFT, cfg => cfg.MapFrom(x => x.SWIFT));

    }
}