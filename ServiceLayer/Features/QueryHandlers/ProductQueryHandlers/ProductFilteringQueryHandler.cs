using DomainLayer.Entities;
using DomainLayer.Entities.Products;
using InfrastructureLayer.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Features.Queries.ProductQueries;
using ServiceLayer.Models;

namespace ServiceLayer.Features.QueryHandlers.ProductQueryHandlers;

public class ProductFilteringQueryHandler : IRequestHandler<ProductFilteringQuery, PagedList<ProductModel>>
{
    private readonly DbSet<Product> _dbSet;
    public ProductFilteringQueryHandler(ECommerceDbContext db)
    {
        var dbSet = db.Set<Product>();
        _dbSet = dbSet;
    }
    public async Task<PagedList<ProductModel>> Handle(ProductFilteringQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Product> products = _dbSet;

        if (request.filter.BrandIds!.Any())
        {
            products = products.Where(x => request.filter.BrandIds!.Contains(x.Brand!.Id));
        }

        if (request.filter.CategoryIds!.Any())
        {
            products = products.Where(x => request.filter.CategoryIds!.Contains(x.Category!.Id));
        }

            if (request.filter.Condition!.Any())
            {
                products = products.Where(x => request.filter.Condition!.Contains(x.Condition));
            }

        if (request.filter.StockStatus!.HasValue)
        {
            products = products.Where(x => request.filter.StockStatus == x.Status);
        }

            if (request.filter.MinPrice.HasValue)
            {
                products = products.Where(x => x.Price >= request.filter.MinPrice);
            }

        if (request.filter.MaxPrice.HasValue)
        {
            products = products.Where(x => x.Price <= request.filter.MaxPrice);
        }

            var productModelsQuery = products.Select(b => new ProductModel
            {
                Id = b.Id,
                Name = b.Name,
                Price = b.Price,
                Status = b.Status,
                Condition = b.Condition,
                Description = b.Description
            });

        var productsQuery = await PagedList<ProductModel>.CreateAsync(productModelsQuery, request.Page, request.PageSize);

        return productsQuery;
    }
}
