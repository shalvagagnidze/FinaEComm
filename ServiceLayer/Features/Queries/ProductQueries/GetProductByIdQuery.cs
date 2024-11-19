using MediatR;
using ServiceLayer.Models;

namespace ServiceLayer.Features.Queries.ProductQueries;

public record GetProductByIdQuery(Guid id) : IRequest<ProductResponseModel>;
