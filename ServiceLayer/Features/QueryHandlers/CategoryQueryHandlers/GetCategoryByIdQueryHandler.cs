using AutoMapper;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Queries.CategoryQueries;
using ServiceLayer.Models;

namespace ServiceLayer.Features.QueryHandlers.CategoryQueryHandlers;

internal class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<CategoryModel> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var model = await _unitOfWork.CategoryRepository.GetByIdAsync(request.id);

        var category = _mapper.Map<CategoryModel>(model);

        return category;
    }
}
