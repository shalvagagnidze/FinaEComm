using DomainLayer.Common.Enums;
using MediatR;

namespace ServiceLayer.Features.Commands.ProductCommands;

public record CreateProductCommand(string Name,decimal Price,Gender Gender, List<ProductSize> Size, List<Specification> Specifications, Condition Condition,string Description, Guid CategoryId, Guid BrandId,List<string> Images) : IRequest<Guid>;
