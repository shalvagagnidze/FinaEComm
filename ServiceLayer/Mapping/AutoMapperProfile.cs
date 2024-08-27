using AutoMapper;
using DomainLayer.Entities;
using ServiceLayer.Models;

namespace ServiceLayer.Mapping;

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
    }
}
