using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Commands.Brand;
using ServiceLayer.Features.Queries;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class BrandService : BaseService,IBrandService
    {
        private readonly ISender _sender;
        public BrandService(IUnitOfWork unitOfWork, IMapper mapper, ISender sender) : base(unitOfWork, mapper)
        {
            _sender = sender;
        }

        public async Task<IEnumerable<BrandModel>> GetAllAsync()
        {
            var brands = await _sender.Send(new GetAllBrandsQuery());

            if(brands is null)
            {
                return Enumerable.Empty<BrandModel>();
            }
            return brands;
        }

        public async Task<BrandModel> GetByIdAsync(Guid id)
        {
            var brand = await _sender.Send(new GetBrandByIdQuery(id));

            return brand;
        }

        public async Task<Guid> AddAsync(CreateBrandCommand command)
        {
           var brandId = await _sender.Send(command);
           return brandId;
        }

        public async Task UpdateAsync(BrandModel model)
        {
            if(model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var existingBrand = await _unitOfWork.BrandRepository.GetByIdAsync(model.Id);

            if(existingBrand is null)
            {
                throw new ArgumentNullException(nameof(Brand));
            }

            _mapper.Map(model, existingBrand);
            _unitOfWork.BrandRepository.Update(existingBrand);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.BrandRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
