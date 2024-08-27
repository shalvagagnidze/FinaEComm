using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Commands.ProductCommands;

namespace ServiceLayer.Features.CommandHandlers.ProductHandlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.model.Id); 

        if (product is null)
        {
            throw new ArgumentNullException(nameof(product), "Product not found");
        }

        _unitOfWork.ProductRepository.Delete(product);

        product.DeleteTime = DateTime.UtcNow;

        _unitOfWork.ProductRepository.Update(product);

        await _unitOfWork.SaveAsync();
    }
}
