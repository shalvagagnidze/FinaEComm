using MediatR;
using ServiceLayer.Models;

namespace ServiceLayer.Features.Queries.ProductQueries;

public record ProductFilteringQuery(FilterModel filter, int Page, int PageSize) : IRequest<PagedList<ProductModel>>;
