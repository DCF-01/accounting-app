using Accounting.Core.Requests;
using Accounting.Infrastructure.Models;
using AutoMapper;

namespace Accounting.Core.Mappings;

public class MasterCompanyProfile : Profile
{
    public MasterCompanyProfile()
    {
        CreateMap<MasterCompany, MasterCompanyGet>().ReverseMap();
        
    }
}