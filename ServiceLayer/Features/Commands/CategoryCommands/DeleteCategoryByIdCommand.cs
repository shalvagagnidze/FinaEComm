using MediatR;

namespace ServiceLayer.Features.Commands.CategoryCommands;

public record DeleteCategoryByIdCommand(Guid id) : IRequest;
