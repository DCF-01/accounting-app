using Accounting.Core.Requests;
using Accounting.Infrastructure.Models;
using AutoMapper;

namespace Accounting.Core.Mappings;

public class MasterUserProfile : Profile
{
    public MasterUserProfile()
    {
        CreateMap<MasterCompany, MasterCompanyGet>().ReverseMap();
        
    }
}