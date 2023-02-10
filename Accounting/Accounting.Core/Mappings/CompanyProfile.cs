using Accounting.Core.Requests;
using Accounting.Infrastructure.Models;
using AutoMapper;

namespace Accounting.Core.Mappings
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<CompanyPut, Company>();

            CreateMap<CompanyPost, Company>();

            CreateMap<Company, CompanyGet>();
        }

    }
}
