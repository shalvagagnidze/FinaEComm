using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Queries.CategoryQueries;
using ServiceLayer.Models;

namespace ServiceLayer.Features.QueryHandlers.CategoryQueryHandlers
{
    public class GetSubCategoriesQueryHandler : IRequestHandler<GetSubCategoriesQuery, IEnumerable<CategoryModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetSubCategoriesQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryModel>> Handle(GetSubCategoriesQuery request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.id);
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

            var allCategoryNames = GetCategoryAndSubCategoryNames(category, categories);

            var mappedCategories = _mapper.Map<IEnumerable<CategoryModel>>(allCategoryNames);

            return mappedCategories;
        }

        private IEnumerable<Category> GetCategoryAndSubCategoryNames(Category category, IEnumerable<Category> allCategories)
        {
            List<Category> categories = new List<Category> { category };

            AddSubCategoryNames(category, categories, allCategories);
            
            return categories;
        }  

        private void AddSubCategoryNames(Category category, List<Category> categoryNames, IEnumerable<Category> allCategories)
        {
            var subCategories = allCategories.Where(g => g.ParentId == category.Id && !g.IsDeleted).ToList();
            foreach (var subCategory in subCategories)
            {
                categoryNames.Add(subCategory);
                AddSubCategoryNames(subCategory, categoryNames, allCategories);
            }
        }
    }
}
