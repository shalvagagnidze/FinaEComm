using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Entities.Facets;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Queries.CategoryQueries;
using ServiceLayer.Features.Queries.FacetQueries;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.QueryHandlers.FacetQueryHandlers
{
    public class GetSubFacetValuesQueryHandler : IRequestHandler<GetSubFacetValuesQuery, IEnumerable<FacetValueModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetSubFacetValuesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<FacetValueModel>> Handle(GetSubFacetValuesQuery request, CancellationToken cancellationToken)
        {
            var facetValue = await _unitOfWork.FacetValueRepository.GetByIdAsync(request.id);
            var facetValues = await _unitOfWork.FacetValueRepository.GetAllAsync();

            var allFacetValues = GetFacetAndSubFacetValueNames(facetValue, facetValues);

            var mappedFacetValues = _mapper.Map<IEnumerable<FacetValueModel>>(allFacetValues);

            return mappedFacetValues;
        }

        private IEnumerable<FacetValue> GetFacetAndSubFacetValueNames(FacetValue facetValue, IEnumerable<FacetValue> allFacetValues)
        {
            List<FacetValue> facetValues = new List<FacetValue> { facetValue };

            AddSubFacetValueNames(facetValue, facetValues, allFacetValues);

            return facetValues;
        }

        private void AddSubFacetValueNames(FacetValue facetValue, List<FacetValue> facetValues, IEnumerable<FacetValue> allFacetValues)
        {
            var subFacetValues = allFacetValues.Where(g => g.ParentId == facetValue.Id).ToList();
            foreach (var subFacetValue in subFacetValues)
            {
                facetValues.Add(subFacetValue);
                AddSubFacetValueNames(subFacetValue, facetValues, allFacetValues);
            }
        }
    }
}
