using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Commands.BrandCommands;

namespace ServiceLayer.Features.CommandHandlers.BrandHandlers;

public class DeleteBrandByIdCommandHandler : IRequestHandler<DeleteBrandByIdCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteBrandByIdCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(DeleteBrandByIdCommand request, CancellationToken cancellationToken)
    {
        var brand = await _unitOfWork.BrandRepository.GetByIdAsync(request.Id);

        if (brand is null)
        {
            throw new ArgumentNullException(nameof(brand), "Brand not found");
        }

        await _unitOfWork.BrandRepository.DeleteByIdAsync(request.Id);
        
        brand.DeleteTime = DateTime.UtcNow;

        _unitOfWork.BrandRepository.Update(brand);

        await _unitOfWork.SaveAsync();
  
    }
}
