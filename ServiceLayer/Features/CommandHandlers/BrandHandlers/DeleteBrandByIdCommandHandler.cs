using AutoMapper;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Commands.BrandCommands;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.CommandHandlers.BrandHandlers
{
    public class DeleteBrandByIdCommandHandler : IRequestHandler<DeleteBrandByIdCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteBrandByIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task Handle(DeleteBrandByIdCommand request, CancellationToken cancellationToken)
        {
            var brand = await _unitOfWork.BrandRepository.GetByIdAsync(request.Id);

            if (brand is null)
            {
                throw new ArgumentNullException(nameof(brand), "Brand not found");
            }

            var model = _mapper.Map<BrandModel>(brand);

            await _unitOfWork.BrandRepository.DeleteByIdAsync(request.Id);

            await _unitOfWork.SaveAsync();
      
        }
    }
}
