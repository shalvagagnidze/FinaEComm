using AutoMapper;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Queries.ProductQueries;
using ServiceLayer.Models;

namespace ServiceLayer.Features.QueryHandlers.ProductQueryHandlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponseModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductResponseModel>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var models = await _unitOfWork.ProductRepository.GetAllAsync();

            if (models is null)
            {
                return Enumerable.Empty<ProductResponseModel>();
            }

            var products = _mapper.Map<IEnumerable<ProductResponseModel>>(models);

            return products;
        }
    }
}
