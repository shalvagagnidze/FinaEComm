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
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, BrandModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BrandModel> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var existingBrand = await _unitOfWork.BrandRepository.GetByIdAsync(request.model.Id);

            if (existingBrand is null)
            {
                throw new ArgumentNullException(nameof(existingBrand),"Brand not found");
            }

            existingBrand.Name = request.model.Name;
            existingBrand.Origin = request.model.Origin;
            existingBrand.Description = request.model.Description;

            _unitOfWork.BrandRepository.Update(existingBrand);
            await _unitOfWork.SaveAsync();

            var brand = _mapper.Map<BrandModel>(existingBrand);

            return brand;
        }
    }
}
