using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Commands.ProductCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.CommandHandlers.ProductHandlers
{
    public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteProductByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.id);

            if(product is null)
            {
                throw new ArgumentNullException(nameof(product), "Product not found");
            }

            await _unitOfWork.ProductRepository.DeleteByIdAsync(request.id);

            product.DeleteTime = DateTime.UtcNow;

            _unitOfWork.ProductRepository.Update(product);

            await _unitOfWork.SaveAsync();
        }
    }
}
