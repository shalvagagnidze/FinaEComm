using AutoMapper;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Queries.ProductQueries;
using ServiceLayer.Models;

namespace ServiceLayer.Features.QueryHandlers.ProductQueryHandlers;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ProductModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var model = await _unitOfWork.ProductRepository.GetByIdAsync(request.id);

        var product = _mapper.Map<ProductModel>(model);

        return product;
    }
}
