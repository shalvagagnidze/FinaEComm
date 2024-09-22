using AutoMapper;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Commands.ProductCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Entities.Products;

namespace ServiceLayer.Features.CommandHandlers.ProductHandlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var existingProduct = await _unitOfWork.ProductRepository.GetByIdAsync(request.model.Id);

            if (existingProduct is null)
            {
                throw new ArgumentNullException(nameof(existingProduct), "Product not found");
            }

            existingProduct.Name = request.model.Name;
            existingProduct.Price = request.model.Price;
            existingProduct.Status = request.model.Status;
            existingProduct.Description = request.model.Description;

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
}
