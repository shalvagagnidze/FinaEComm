using AutoMapper;
using DomainLayer.Common.Enums;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using ServiceLayer.Features.Commands.ProductCommands;
using ServiceLayer.Models;
using System;

namespace ServiceLayer.Features.CommandHandlers.ProductHandlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _environment;
    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,IWebHostEnvironment environment)
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
            Gender = request.Gender,
            Size = request.Size,
            Status = StockStatus.InStock,
            Specifications = request.Specifications,
            Condition = request.Condition,
            Description = request.Description,
            Images = images
        };

        var product = _mapper.Map<Product>(model);
        var brand = await _unitOfWork.BrandRepository.GetByIdAsync(request.BrandId);
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);

        product.Brand = brand;
        product.Category = category;
        product.CreatedDate = DateTime.UtcNow;

        await _unitOfWork.ProductRepository.AddAsync(product);
        await _unitOfWork.SaveAsync();

        return product.Id;
    }
}
