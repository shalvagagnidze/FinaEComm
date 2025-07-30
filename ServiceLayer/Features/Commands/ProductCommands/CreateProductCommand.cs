using DomainLayer.Common.Enums;
using DomainLayer.Entities.Products;
using MediatR;
using ServiceLayer.Models;

namespace ServiceLayer.Features.Commands.ProductCommands
{
    public record CreateProductCommand(string Name,decimal Price,decimal? DiscountPrice,Condition Condition, ICollection<ProductFacetValueModel>? ProductFacetValues, string Description, Guid CategoryId, Guid BrandId, List<string> Images,bool? IsActive) : IRequest<Guid>;

}
