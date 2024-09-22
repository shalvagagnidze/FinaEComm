using MediatR;
using ServiceLayer.Models;

namespace ServiceLayer.Features.Commands.BrandCommands;

public record UpdateBrandCommand(BrandModel model) : IRequest<Unit>;
