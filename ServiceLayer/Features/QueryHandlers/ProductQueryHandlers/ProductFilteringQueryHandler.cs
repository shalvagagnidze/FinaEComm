using DomainLayer.Entities;
using InfrastructureLayer.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Features.Queries.CategoryQueries;
using ServiceLayer.Features.Queries.ProductQueries;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.QueryHandlers.ProductQueryHandlers
{
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
            IQueryable<Product> productQuery = _dbSet;

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                productQuery = productQuery.Where(b => b.Name!.ToLower().Contains(request.SearchTerm.ToLower()));
            }

            if (request.SortOrder?.ToLower() == "desc")
            {
                productQuery = productQuery.OrderByDescending(GetSortProperty(request));
            }
            else
            {
                productQuery = productQuery.OrderBy(GetSortProperty(request));
            }

            var productModelsQuery = productQuery.Select(b => new ProductModel
            {
                Id = b.Id,
                Name = b.Name,
                Price = b.Price,
                Sex = b.Sex,
                Size = b.Size,
                Status = b.Status,
                Condition = b.Condition,
                Description = b.Description
            });

            var products = await PagedList<ProductModel>.CreateAsync(productModelsQuery, request.Page, request.PageSize);

            return products;
        }

        private static Expression<Func<Product, object>> GetSortProperty(ProductFilteringQuery request) =>
      request.SortColumn?.ToLower() switch
      {
          "name" => product => product.Name!,
          "price" => product => product.Price,
          "size" => product => product.Size,
          _ => product => product.Id
      };
    }
}
