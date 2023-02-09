using System.Text.RegularExpressions;
using Accounting.Core.Requests;
using Accounting.Infrastructure.Models;
using AutoMapper;

namespace Accounting.Core.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductGet, Product>()
            .ForMember(x => x.MasterCompanyId, cfg => cfg.Ignore());
        CreateMap<Product, ProductGet>()
            .ForMember(x => x.MasterCompanyId, cfg => cfg.Ignore());

        CreateMap<ProductPost, Product>()
            .ForMember(x => x.MasterCompanyId, cfg => cfg.Ignore())
            .ForMember(x => x.GalleryImages, cfg => cfg.Ignore())
            .ForMember(x => x.Groups,
                cfg =>
                    cfg.MapFrom(x =>
                        x.GroupIds.Select(id => new Group { GroupId = id })
                    )
            )
            .ForMember(x => x.Variations,
                cfg =>
                    cfg.MapFrom(x =>
                        x.VariationIds.Select(id => new Variation { VariationId = id })
                    )
            )
            .ForMember(x => x.Currency,
                cfg =>
                    cfg.MapFrom(x =>
                        new Currency { CurrencyId = x.CurrencyId })
            )
            .ForMember(x => x.VAT,
                cfg =>
                    cfg.MapFrom(x =>
                        new VAT { VATId = x.VATId })
            )
            .ForMember(x => x.Spec,
                cfg =>
                    cfg.MapFrom(x =>
                        new Spec { SpecId = x.SpecId })
            );
        
        CreateMap<ProductPut, Product>()
            .ForMember(x => x.MasterCompanyId, cfg => cfg.Ignore())
            .ForMember(x => x.GalleryImages, cfg => cfg.Ignore())
            .ForMember(x => x.ProductId, cfg => cfg.MapFrom(x => x.ProductId))
            .ForMember(x => x.Groups,
                cfg =>
                    cfg.MapFrom(x =>
                        x.GroupIds.Select(id => new Group { GroupId = id })
                    )
            )
            .ForMember(x => x.Variations,
                cfg =>
                    cfg.MapFrom(x =>
                        x.VariationIds.Select(id => new Variation { VariationId = id })
                    )
            )
            .ForMember(x => x.Currency,
                cfg =>
                    cfg.MapFrom(x =>
                        new Currency { CurrencyId = x.CurrencyId })
            )
            .ForMember(x => x.VAT,
                cfg =>
                    cfg.MapFrom(x =>
                        new VAT { VATId = x.VATId })
            )
            .ForMember(x => x.Spec,
                cfg =>
                    cfg.MapFrom(x =>
                        new Spec { SpecId = x.SpecId })
            );

        CreateMap<Product, ProductPost>()
            .ForMember(x => x.MasterCompanyId, cfg => cfg.Ignore())
            .ForMember(x => x.GroupIds, cfg => cfg.Ignore())
            .ForMember(x => x.VariationIds, cfg => cfg.Ignore());
    }
}