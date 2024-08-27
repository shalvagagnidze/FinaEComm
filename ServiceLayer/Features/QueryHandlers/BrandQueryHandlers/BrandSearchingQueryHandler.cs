using DomainLayer.Entities;
using InfrastructureLayer.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Features.Queries.BrandQueries;
using ServiceLayer.Models;
using System.Linq.Expressions;

namespace ServiceLayer.Features.QueryHandlers.BrandQueryHandlers;

public class BrandSearchingQueryHandler : IRequestHandler<BrandSearchingQuery, PagedList<BrandModel>>
{
    private readonly DbSet<Brand> _dbSet;
    public BrandSearchingQueryHandler(ECommerceDbContext db)
    {
        var dbSet = db.Set<Brand>();
        _dbSet = dbSet;
    }
    public async Task<PagedList<BrandModel>> Handle(BrandSearchingQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Brand> brandsQuery = _dbSet;


        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            brandsQuery = brandsQuery.Where(b => b.Name!.ToLower().Contains(request.SearchTerm.ToLower()));
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
            Description = b.Description
        });

        var brands = await PagedList<BrandModel>.CreateAsync(brandModelsQuery,request.Page,request.PageSize);

        return brands;

    }

    private static Expression<Func<Brand, object>> GetSortProperty(BrandSearchingQuery request) =>
   request.SortColumn?.ToLower() switch
   {
       "name" => brand => brand.Name!,
       "origin" => brand => brand.Origin!,
       _ => brand => brand.Id
   };
}
