using MediatR;
using ServiceLayer.Models;

namespace ServiceLayer.Features.Queries.ProductQueries;

public record GetAllProductsQuery : IRequest<IEnumerable<ProductModel>>;
