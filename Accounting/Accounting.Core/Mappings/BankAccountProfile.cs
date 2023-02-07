using Accounting.Core.Requests;
using Accounting.Infrastructure.Models;
using AutoMapper;

namespace Accounting.Core.Mappings;

public class BankAccountProfile : Profile
{
    public BankAccountProfile()
    {
        CreateMap<BankAccountPost, BankAccount>();

        CreateMap<BankAccountPut, BankAccount>();

        CreateMap<BankAccount, BankAccountGet>();
    }
}