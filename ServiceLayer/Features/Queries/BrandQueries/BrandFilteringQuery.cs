using MediatR;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.Queries.BrandQueries
{
    public record BrandFilteringQuery(string? SearchTerm,string? SortColumn,string? SortOrder,int Page,int PageSize) : IRequest<PagedList<BrandModel>>;

}
