using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Commands.CategoryCommands;

namespace ServiceLayer.Features.CommandHandlers.CategoryHandlers;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(request.model.Id);

        if(existingCategory is null)
        {
            throw new ArgumentNullException(nameof(existingCategory), "Category not found");
        }

        existingCategory.Name = request.model.Name;
        existingCategory.Description = request.model.Description;
        existingCategory.ParentId = request.model.ParentId;

        _unitOfWork.CategoryRepository.Update(existingCategory);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}
