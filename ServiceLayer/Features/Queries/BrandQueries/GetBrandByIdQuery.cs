using MediatR;
using ServiceLayer.Models;

namespace ServiceLayer.Features.Queries.BrandQueries;

public record GetBrandByIdQuery(Guid id) : IRequest<BrandModel>;
