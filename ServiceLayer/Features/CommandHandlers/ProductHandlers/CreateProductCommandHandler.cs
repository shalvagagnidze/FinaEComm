using AutoMapper;
using DomainLayer.Common.Enums;
using DomainLayer.Entities;
using DomainLayer.Entities.Products;
using DomainLayer.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Hosting;
using ServiceLayer.Features.Commands.ProductCommands;
using ServiceLayer.Models;
using System;
using Microsoft.Extensions.Hosting;

namespace ServiceLayer.Features.CommandHandlers.ProductHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment environment)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _environment = environment;
        }
        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var images = new List<string>();

            var contentPath = _environment.ContentRootPath;
            var path = Path.Combine(contentPath, "Images", $"{request.Name}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var sourcePath = request.Images;

            foreach (var image in sourcePath)
            {
                if (File.Exists(image))
                {
                    var fileName = Path.GetFileName(image);
                    var destinationPath = Path.Combine(path, fileName);

                    File.Move(image, destinationPath);
                    images.Add(destinationPath);
                }

            }

            var model = new ProductModel
            {
                Name = request.Name,
                Price = request.Price,
                DiscountPrice = request.DiscountPrice,
                Status = StockStatus.InStock,
                Condition = request.Condition,
                Description = request.Description,
                Images = images,
                IsActive = request.IsActive
            };

            var product = _mapper.Map<Product>(model);
            var brand = await _unitOfWork.BrandRepository.GetByIdAsync(request.BrandId);
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);

            product.Brand = brand;
            product.Category = category;
            product.CreatedDate = DateTime.UtcNow;
            if (product.ProductFacetValues is null || !product.ProductFacetValues.Any())
            {
                product.ProductFacetValues = [];
            }

            await _unitOfWork.ProductRepository.AddAsync(product);
            foreach (var facet in request.ProductFacetValues!)
            {
                var productFacet = new DomainLayer.Entities.Products.ProductFacetValue
                {
                    FacetValueId = facet.FacetValueId,
                    ProductId = product.Id                  
                };

                product.ProductFacetValues!.Add(productFacet);
            }

            await _unitOfWork.SaveAsync();

            return product.Id;
        }
    }
}
