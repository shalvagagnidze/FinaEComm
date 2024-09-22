using MediatR;
using ServiceLayer.Models;

namespace ServiceLayer.Features.Commands.ProductCommands;

public record DeleteProductCommand(ProductModel model) : IRequest;
