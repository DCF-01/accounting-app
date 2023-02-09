using Accounting.Core.Requests;
using Accounting.Infrastructure.Models;
using AutoMapper;

namespace Accounting.Core.Mappings;

public class VariationProfile : Profile
{
    public VariationProfile()
    {
        CreateMap<Variation, VariationGet>().ReverseMap();
    }
}