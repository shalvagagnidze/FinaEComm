using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Commands.ProductCommands;
using ServiceLayer.Interfaces;

namespace ServiceLayer.Features.CommandHandlers.ProductHandlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileService _fileService;
    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IFileService fileService)
    {
        _unitOfWork = unitOfWork;
        _fileService = fileService;
    }
    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await _unitOfWork.ProductRepository.GetByIdAsync(request.model.Id);

        if (existingProduct is null)
        {
            throw new ArgumentNullException(nameof(existingProduct), "Product not found");
        }


        var oldImages = existingProduct.Images;

        existingProduct.Name = request.model.Name;
        existingProduct.Price = request.model.Price;
        existingProduct.Status = request.model.Status;
        existingProduct.Description = request.model.Description;
        existingProduct.Images = request.model.Images;

        if (request.model.Images != null)
        {
            foreach(var image in oldImages!)
            {
                _fileService.DeleteFile(image);
            }              
        }
            


            if (request.model.ProductFacetValues != null)
            {
                var productFacetValues = request.model.ProductFacetValues.Select(facetValue => new ProductFacetValue
                {
                    FacetValueId = facetValue.FacetValueId,
                    ProductId = request.model.Id
                });

                existingProduct.ProductFacetValues = productFacetValues.ToList();
            }

            _unitOfWork.ProductRepository.Update(existingProduct);

        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}
