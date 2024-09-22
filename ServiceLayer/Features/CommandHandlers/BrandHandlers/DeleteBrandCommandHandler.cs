using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Commands.BrandCommands;

namespace ServiceLayer.Features.CommandHandlers.BrandHandlers;

public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteBrandCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = await _unitOfWork.BrandRepository.GetByIdAsync(request.brand.Id);

        if(brand is null)
        {
            throw new ArgumentNullException(nameof(brand),"Brand not found");
        }

        _unitOfWork.BrandRepository.Delete(brand);

        brand.DeleteTime = DateTime.UtcNow;

        _unitOfWork.BrandRepository.Update(brand);

        await _unitOfWork.SaveAsync();

    }

}
