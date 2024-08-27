using DomainLayer.Entities;
using InfrastructureLayer.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Features.Queries.CategoryQueries;
using ServiceLayer.Models;
using System.Linq.Expressions;

namespace ServiceLayer.Features.QueryHandlers.CategoryQueryHandlers;

public class CategoryFilteringQueryHandler : IRequestHandler<CategoryFilteringQuery, PagedList<CategoryModel>>
{
    private readonly DbSet<Category> _dbSet;
    public CategoryFilteringQueryHandler(ECommerceDbContext db)
    {
        var dbSet = db.Set<Category>();
        _dbSet = dbSet;
    }
    public async Task<PagedList<CategoryModel>> Handle(CategoryFilteringQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Category> categoryQuery = _dbSet;

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            categoryQuery = categoryQuery.Where(b => b.Name!.ToLower().Contains(request.SearchTerm.ToLower()));
        }

        if (request.SortOrder?.ToLower() == "desc")
        {
            categoryQuery = categoryQuery.OrderByDescending(GetSortProperty(request));
        }
        else
        {
            categoryQuery = categoryQuery.OrderBy(GetSortProperty(request));
        }

        var categoryModelsQuery = categoryQuery.Select(b => new CategoryModel
        {
            Id = b.Id,
            Name = b.Name,
            Description = b.Description
        });

        var categories = await PagedList<CategoryModel>.CreateAsync(categoryModelsQuery, request.Page, request.PageSize);

        return categories;
    }

    private static Expression<Func<Category, object>> GetSortProperty(CategoryFilteringQuery request) =>
  request.SortColumn?.ToLower() switch
  {
      "name" => categories => categories.Name!,
      _ => categories => categories.Id
  };
}
