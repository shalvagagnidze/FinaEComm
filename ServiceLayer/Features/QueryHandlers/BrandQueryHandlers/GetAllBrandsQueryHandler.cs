using AutoMapper;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Queries.BrandQueries;
using ServiceLayer.Models;

namespace ServiceLayer.Features.QueryHandlers.BrandQueryHandlers;

public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, IEnumerable<BrandModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllBrandsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IEnumerable<BrandModel>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var models = await _unitOfWork.BrandRepository.GetAllAsync();

        if (models is null)
        {
            return Enumerable.Empty<BrandModel>();
        }

        var brands = _mapper.Map<IEnumerable<BrandModel>>(models);

        return brands;
    }
}
