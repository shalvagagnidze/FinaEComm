using MediatR;
using ServiceLayer.Models;

namespace ServiceLayer.Features.Queries.CategoryQueries;

public record GetAllCategoriesQuery : IRequest<IEnumerable<CategoryModel>>;

