using MediatR;
using ServiceLayer.Models;

namespace ServiceLayer.Features.Commands.CategoryCommands;

public record UpdateCategoryCommand(CategoryModel model) : IRequest<Unit>;
