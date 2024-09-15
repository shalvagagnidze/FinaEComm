using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Entities.Facets;
using DomainLayer.Entities.Products;
using ServiceLayer.Features.Commands.FacetCommands;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            CreateMap<Product, ProductResponseModel>();
            CreateMap<ProductFacetValue, ProductFacetValueResponseModel>()
                .ForMember(fv => fv.FacetName, opt => opt.MapFrom(productFacetValue => productFacetValue.FacetValue.Facet.Name))
                .ForMember(fv => fv.FacetValue, opt => opt.MapFrom(productFacetValue => productFacetValue.FacetValue.Value));
        }
    }
}
