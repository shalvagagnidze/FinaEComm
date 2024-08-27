using MediatR;
using ServiceLayer.Models;

namespace ServiceLayer.Features.Queries.BrandQueries;

public record GetAllBrandsQuery : IRequest<IEnumerable<BrandModel>>;
