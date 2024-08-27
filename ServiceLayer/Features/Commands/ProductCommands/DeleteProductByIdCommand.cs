using MediatR;

namespace ServiceLayer.Features.Commands.ProductCommands;

public record DeleteProductByIdCommand(Guid id) : IRequest;

