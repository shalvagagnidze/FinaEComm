using MediatR;
using ServiceLayer.Models;

namespace ServiceLayer.Features.Queries.ProductQueries;

public record ProductSearchingQuery(string? SearchTerm, string? SortColumn, string? SortOrder, int Page, int PageSize) : IRequest<PagedList<ProductModel>>;
