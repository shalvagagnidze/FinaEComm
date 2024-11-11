using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Entities.Facets;
using DomainLayer.Entities.Products;
using ServiceLayer.Features.Commands.FacetCommands;
using ServiceLayer.Models;

namespace ServiceLayer.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Brand, BrandModel>();
            CreateMap<BrandModel, Brand>();
            CreateMap<Category, CategoryModel>();
            CreateMap<CategoryModel, Category>();
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();
            CreateMap<FacetValueModel, FacetValue>();
            CreateMap<FacetValue, FacetValueModel>();
            CreateMap<FacetModel, Facet>();
            CreateMap<Facet, FacetModel>();
            CreateMap<ProductFacetValue, ProductFacetValueModel>();
            CreateMap<ProductFacetValueModel, ProductFacetValue>();
            CreateMap<Product, ProductResponseModel>()
                    .ForMember(prodResp => prodResp.Brand, prod => prod.MapFrom(product => product.Brand))
                    .ForMember(prodResp => prodResp.Category, cat => cat.MapFrom(product => product.Category))
                    .ReverseMap();
            CreateMap<ProductFacetValue, ProductFacetValueResponseModel>()
                .ForMember(fv => fv.FacetName, opt => opt.MapFrom(productFacetValue => productFacetValue.FacetValue!.Facet!.Name))
                .ForMember(fv => fv.FacetValue, opt => opt.MapFrom(productFacetValue => productFacetValue.FacetValue!.Value));
        }
    }
}
