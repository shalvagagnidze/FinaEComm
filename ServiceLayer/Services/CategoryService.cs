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
    public class CategoryService : BaseService,ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }

        public async Task<IEnumerable<CategoryModel>> GetAllAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

            if (categories is null)
            {
                return Enumerable.Empty<CategoryModel>();
            }
            return _mapper.Map<IEnumerable<CategoryModel>>(categories);
        }

        public async Task<CategoryModel> GetByIdAsync(Guid id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);

            return _mapper.Map<CategoryModel>(category);
        }

        public async Task AddAsync(CategoryModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var category = _mapper.Map<Category>(model);

            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(CategoryModel model)
        {
            if (model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var existingCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(model.Id);

            if (existingCategory is null)
            {
                throw new ArgumentNullException(nameof(Category));
            }

            _mapper.Map(model, existingCategory);
            _unitOfWork.CategoryRepository.Update(existingCategory);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            await _unitOfWork.CategoryRepository.DeleteByIdAsync(id);
            await _unitOfWork.SaveAsync();
        }
    }
}
