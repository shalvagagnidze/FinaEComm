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
    public class GetAllFacetsWithValueQueryHandler : IRequestHandler<GetAllFacetsWithValuesQuery, IEnumerable<FacetModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetAllFacetsWithValueQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<FacetModel>> Handle(GetAllFacetsWithValuesQuery request, CancellationToken cancellationToken)
        {
            var models = await _unitOfWork.FacetRepository.GetAllAsync();

            if (models is null)
            {
                return Enumerable.Empty<FacetModel>();
            }

            models = models.Where(f => f.Categories.Any())
                .Where(f => f.Categories.Any(c => c.Id == request.categoryId));

            var facets = _mapper.Map<IEnumerable<FacetModel>>(models);

            return facets;
        }
    }
}
