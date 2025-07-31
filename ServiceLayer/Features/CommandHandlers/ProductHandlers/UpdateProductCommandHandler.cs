using DomainLayer.Entities.Products;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Commands.ProductCommands;
using ServiceLayer.Interfaces;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

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
        existingProduct.DiscountPrice = request.model.DiscountPrice;
        existingProduct.Status = request.model.Status;
        existingProduct.Description = request.model.Description;
        existingProduct.Images = request.model.Images;
        existingProduct.Condition = request.model.Condition;
        existingProduct.IsActive = request.model.IsActive;
        existingProduct.IsLiquidated = request.model.IsLiquidated;
        existingProduct.IsComingSoon = request.model.IsComingSoon;
        existingProduct.IsNewArrival = request.model.IsNewArrival;

        var brand = await _unitOfWork.BrandRepository.GetByIdAsync(request.model.BrandId);
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.model.CategoryId);

        existingProduct.Brand = brand;
        existingProduct.Category = category;

        //if (request.model.Images != null)
        //{
        //    foreach(var image in oldImages!)
        //    {
        //        _fileService.DeleteFile(image);
        //    }              
        //}

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
