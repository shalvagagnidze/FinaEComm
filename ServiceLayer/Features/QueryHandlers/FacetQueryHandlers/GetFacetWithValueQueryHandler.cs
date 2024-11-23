using AutoMapper;
using DomainLayer.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Features.Queries.FacetQueries;
using ServiceLayer.Features.Queries.ProductQueries;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.QueryHandlers.FacetQueryHandlers
{
    public class GetFacetWithValueQueryHandler : IRequestHandler<GetFacetWithValuesQuery, FacetModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetFacetWithValueQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<FacetModel> Handle(GetFacetWithValuesQuery request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.FacetRepository.GetByIdAsync(request.facetId);

            if (model is null)
            {
                return new FacetModel();
            }

            var facet = _mapper.Map<FacetModel>(model);

            return facet;
        }
    }
}
