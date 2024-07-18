using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
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
        public BrandService(IUnitOfWork unitOfWork,IMapper mapper) : base(unitOfWork,mapper)
        {
            
        }

        public async Task<IEnumerable<BrandModel>> GetAllAsync()
        {
            var brands = await _unitOfWork.BrandRepository.GetAllAsync();

            if(brands is null)
            {
                return Enumerable.Empty<BrandModel>();
            }
            return _mapper.Map<IEnumerable<BrandModel>>(brands);
        }

        public async Task<BrandModel> GetByIdAsync(Guid id)
        {
            var brand = await _unitOfWork.BrandRepository.GetByIdAsync(id);

            return _mapper.Map<BrandModel>(brand);
        }

        public async Task AddAsync(BrandModel model)
        {
            if(model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var brand = _mapper.Map<Brand>(model);

            await _unitOfWork.BrandRepository.AddAsync(brand);
            await _unitOfWork.SaveAsync();
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
