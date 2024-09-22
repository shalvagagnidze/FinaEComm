using AutoMapper;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Queries.BrandQueries;
using ServiceLayer.Models;

namespace ServiceLayer.Features.QueryHandlers.BrandQueryHandlers;

internal class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, BrandModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetBrandByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<BrandModel> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
    {
        var model = await _unitOfWork.BrandRepository.GetByIdAsync(request.id);

        var brand = _mapper.Map<BrandModel>(model);

        return brand;
    }
}
