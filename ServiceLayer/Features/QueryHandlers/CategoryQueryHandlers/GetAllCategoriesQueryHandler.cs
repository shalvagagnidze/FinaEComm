using AutoMapper;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Queries.CategoryQueries;
using ServiceLayer.Models;

namespace ServiceLayer.Features.QueryHandlers.CategoryQueryHandlers;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, IEnumerable<CategoryModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IEnumerable<CategoryModel>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var models = await _unitOfWork.CategoryRepository.GetAllAsync();

        if(models is null)
        {
            return Enumerable.Empty<CategoryModel>();
        }

        var categories = _mapper.Map<IEnumerable<CategoryModel>>(models);

        return categories;
    }
}
