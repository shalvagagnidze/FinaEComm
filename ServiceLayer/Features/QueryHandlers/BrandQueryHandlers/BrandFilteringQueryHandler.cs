using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using InfrastructureLayer.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Features.Queries.BrandQueries;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.QueryHandlers.BrandQueryHandlers
{
    public class BrandFilteringQueryHandler : IRequestHandler<BrandFilteringQuery, PagedList<BrandModel>>
    {
        private readonly DbSet<Brand> _dbSet;
        //private readonly IUnitOfWork _unitOfWork;
        public BrandFilteringQueryHandler(ECommerceDbContext db /*IUnitOfWork unitOfWork*/)
        {
            var dbSet = db.Set<Brand>();
            _dbSet = dbSet;
            //_unitOfWork = unitOfWork;
        }
        public async Task<PagedList<BrandModel>> Handle(BrandFilteringQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Brand> brandsQuery = _dbSet;


            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                brandsQuery = brandsQuery.Where(b => b.Name!.Contains(request.SearchTerm));
            }

            if (request.SortOrder?.ToLower() == "desc")
            {
                brandsQuery = brandsQuery.OrderByDescending(GetSortProperty(request));
            }
            else
            {
                brandsQuery = brandsQuery.OrderBy(GetSortProperty(request));
            }

            var brandModelsQuery = brandsQuery.Select(b => new BrandModel
            {
                Id = b.Id,
                Name = b.Name,
                Origin = b.Origin,
                Description = b.Description,
            });


            var brands = await PagedList<BrandModel>.CreateAsync(brandModelsQuery,request.Page,request.PageSize);

            return brands;
   
        }

        private static Expression<Func<Brand, object>> GetSortProperty(BrandFilteringQuery request) =>
       request.SortColumn?.ToLower() switch
       {
           "name" => brand => brand.Name!,
           "origin" => brand => brand.Origin!,
           _ => brand => brand.Id
       };
    }
}
