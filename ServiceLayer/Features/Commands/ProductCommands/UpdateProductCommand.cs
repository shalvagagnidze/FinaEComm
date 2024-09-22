using MediatR;
using ServiceLayer.Models;

namespace ServiceLayer.Features.Commands.ProductCommands;

public record UpdateProductCommand(ProductModel model) : IRequest<Unit>;
