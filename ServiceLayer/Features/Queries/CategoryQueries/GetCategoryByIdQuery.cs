using MediatR;
using ServiceLayer.Models;

namespace ServiceLayer.Features.Queries.CategoryQueries
{
    public record GetCategoryByIdQuery(Guid id) : IRequest<CategoryModel>;

}
