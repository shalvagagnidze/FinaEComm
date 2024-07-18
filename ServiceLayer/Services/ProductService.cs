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
    public class ProductService : BaseService,IProductService
    {
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public async Task<IEnumerable<ProductModel>> GetAllAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();

            if (products is null)
            {
                return Enumerable.Empty<ProductModel>();
            }
            return _mapper.Map<IEnumerable<ProductModel>>(products);
        }

        public async Task<ProductModel> GetByIdAsync(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);

            return _mapper.Map<ProductModel>(product);
        }

        public async Task AddAsync(ProductModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var product = _mapper.Map<Product>(model);

            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(ProductModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var existingProduct = await _unitOfWork.ProductRepository.GetByIdAsync(model.Id);

            if (existingProduct is null)
            {
                throw new ArgumentNullException(nameof(Product));
            }

            _mapper.Map(model, existingProduct);
            _unitOfWork.ProductRepository.Update(existingProduct);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.ProductRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
