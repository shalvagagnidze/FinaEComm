using MediatR;

namespace ServiceLayer.Features.Commands.CategoryCommands;

public record CreateCategoryCommand(string Name,string Description,Guid? ParentId = null) : IRequest<Guid>;
