using MediatR;
using ServiceLayer.Models;

namespace ServiceLayer.Features.Queries.BrandQueries;

public record BrandSearchingQuery(string? SearchTerm,string? SortColumn,string? SortOrder,int Page,int PageSize) : IRequest<PagedList<BrandModel>>;
