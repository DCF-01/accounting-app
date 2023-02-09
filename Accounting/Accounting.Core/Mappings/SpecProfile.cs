using Accounting.Core.Requests;
using Accounting.Infrastructure.Models;
using AutoMapper;

namespace Accounting.Core.Mappings;

public class SpecProfile : Profile
{
    public SpecProfile()
    {
        CreateMap<Spec, SpecGet>().ReverseMap();
    }
}