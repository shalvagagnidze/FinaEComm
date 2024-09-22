using DomainLayer.Common.Enums;
using DomainLayer.Entities.Products;
using MediatR;

namespace ServiceLayer.Features.Commands.ProductCommands
{
    public record CreateProductCommand(string Name,decimal Price,Condition Condition, ICollection<ProductFacetValueModel>? ProductFacetValues, string Description, Guid CategoryId, Guid BrandId, List<string> Images) : IRequest<Guid>;

}
