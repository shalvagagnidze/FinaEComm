using MediatR;
using ServiceLayer.Models;

namespace ServiceLayer.Features.Commands.BrandCommands;

public record DeleteBrandCommand(BrandModel brand) : IRequest;

