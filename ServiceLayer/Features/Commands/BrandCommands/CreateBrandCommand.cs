using MediatR;

namespace ServiceLayer.Features.Commands.BrandCommands;

public record CreateBrandCommand(string Name, string Origin, string Description) : IRequest<Guid>;
