using DomainLayer.Entities.Facets;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Commands.FacetCommands;
using ServiceLayer.Features.Commands.ProductCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.CommandHandlers.FacetHandlers
{
    public class UpdateFacetCommandHandler : IRequestHandler<UpdateFacetCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateFacetCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateFacetCommand request, CancellationToken cancellationToken)
        {
            var existingFacet = await _unitOfWork.FacetRepository.GetByIdAsync(request.model.Id);

            if (existingFacet is null)
            {
                throw new ArgumentNullException(nameof(existingFacet), "Facet not found");
            }

            existingFacet.Name = request.model.Name;
            existingFacet.DisplayType = request.model.DisplayType;
            existingFacet.IsCustom = request.model.IsCustom;

            if (request.model.FacetValues != null)
            {
                var existingFacetValues = existingFacet.FacetValues.ToList();
                var updatedFacetValueIds = request.model.FacetValues.Select(v => v.Id).ToList();

                foreach (var existingFacetValue in existingFacetValues.ToList())
                {
                    if (!updatedFacetValueIds.Contains(existingFacetValue.Id))
                    {
                        existingFacet.FacetValues.Remove(existingFacetValue);
                    }
                }

                foreach (var facetValueDto in request.model.FacetValues)
                {
                    var existingFacetValue = existingFacetValues.SingleOrDefault(v => v.Id == facetValueDto.Id);
                    if (existingFacetValue != null)
                    {
                        existingFacetValue.Value = facetValueDto.Value;
                    }
                    else
                    {
                        var newFacetValue = new FacetValue
                        {
                            Id = Guid.NewGuid(),
                            Value = facetValueDto.Value,
                            FacetId = existingFacet.Id
                        };
                        existingFacet.FacetValues.Add(newFacetValue);
                    }
                }
            }

            _unitOfWork.FacetRepository.Update(existingFacet);

            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
