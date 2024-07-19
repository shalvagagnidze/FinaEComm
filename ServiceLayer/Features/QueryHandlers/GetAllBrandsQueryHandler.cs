using AutoMapper;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Queries;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.QueryHandlers
{
    public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, IEnumerable<BrandModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllBrandsQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BrandModel>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var models = await _unitOfWork.BrandRepository.GetAllAsync();

            var brands = _mapper.Map<IEnumerable<BrandModel>>(models);

            return brands;  
        }
    }
}
