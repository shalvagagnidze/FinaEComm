using MediatR;
using ServiceLayer.Models;

namespace ServiceLayer.Features.Queries.CategoryQueries;

public record CategoryFilteringQuery(string? SearchTerm, string? SortColumn, string? SortOrder, int Page, int PageSize) : IRequest<PagedList<CategoryModel>>;
