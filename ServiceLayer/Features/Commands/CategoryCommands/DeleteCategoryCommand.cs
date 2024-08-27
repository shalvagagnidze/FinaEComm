using MediatR;
using ServiceLayer.Models;

namespace ServiceLayer.Features.Commands.CategoryCommands;

public record DeleteCategoryCommand(CategoryModel model) : IRequest;
