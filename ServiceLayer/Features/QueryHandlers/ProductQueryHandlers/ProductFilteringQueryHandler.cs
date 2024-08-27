using DomainLayer.Entities;
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

        if (request.filter.Gender!.Any())
        {
            products = products.Where(x => request.filter.Gender!.Contains(x.Gender));
        }

        if (request.filter.ProductSize!.Any())
        {
            products = products.Where(x => x.Size!.Any(s => request.filter.ProductSize!.Contains(s)));
        }

        if (request.filter.Condition!.Any())
        {
            products = products.Where(x => request.filter.Condition!.Contains(x.Condition));
        }

        if (request.filter.StockStatus!.HasValue)
        {
            products = products.Where(x => request.filter.StockStatus == x.Status);
        }

        if (request.filter.Specifications!.Any())
        {
            products = products.Where(x => x.Specifications!.Any(s => request.filter.Specifications!.Contains(s)));
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
            Gender = b.Gender,
            Size = b.Size,
            Status = b.Status,
            Condition = b.Condition,
            Specifications = b.Specifications,
            Description = b.Description
        });

        var productsQuery = await PagedList<ProductModel>.CreateAsync(productModelsQuery, request.Page, request.PageSize);

        return productsQuery;
    }
}
