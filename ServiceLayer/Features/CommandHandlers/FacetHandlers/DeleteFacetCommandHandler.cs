using DomainLayer.Entities.Facets;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Commands.BrandCommands;
using ServiceLayer.Features.Commands.FacetCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.CommandHandlers.FacetHandlers
{
    public class DeleteFacetCommandHandler : IRequestHandler<DeleteFacetCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public DeleteFacetCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteFacetCommand request, CancellationToken cancellationToken)
        {
            var facet = await _unitOfWork.FacetRepository.GetByIdAsync(request.facetId);

            if (facet is null)
            {
                throw new ArgumentNullException(nameof(facet), "Facet not found");
            }

            await _unitOfWork.FacetRepository.DeleteByIdAsync(request.facetId);
            await _unitOfWork.SaveAsync();
        }
    }
}
