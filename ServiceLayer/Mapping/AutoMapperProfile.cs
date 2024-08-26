using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Entities.Products;
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
        }
    }
}
