using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using InfrastructureLayer.Data;
using MediatR;
using ServiceLayer.Features.Commands.Brand;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.CommandHandlers.Brand
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBrandCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var model = new BrandModel
            {
                Name = request.Name,
                Origin = request.Origin,
                Description = request.Description,
            };

            var brand = _mapper.Map<Brand>(model);

            await _unitOfWork.BrandRepository.AddAsync(brand);
            await _unitOfWork.SaveAsync();

            return model.Id;
        }
    }
}
