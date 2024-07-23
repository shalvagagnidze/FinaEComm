using AutoMapper;
using DomainLayer.Common.Enums;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Commands.ProductCommands;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.CommandHandlers.ProductHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var model = new ProductModel
            {
                Name = request.Name,
                Price = request.Price,
                Sex = request.Sex,
                Size = request.Size,
                Status = StockStatus.InStock,
                Condition = request.Condition,
                Description = request.Description,
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
}
