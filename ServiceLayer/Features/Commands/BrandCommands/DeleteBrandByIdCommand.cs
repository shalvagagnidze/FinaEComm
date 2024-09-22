using MediatR;

namespace ServiceLayer.Features.Commands.BrandCommands;

public record DeleteBrandByIdCommand(Guid Id) : IRequest;

