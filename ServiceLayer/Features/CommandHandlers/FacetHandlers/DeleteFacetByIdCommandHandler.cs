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
    public class DeleteFacetByIdCommandHandler : IRequestHandler<DeleteFacetByIdCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteFacetByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteFacetByIdCommand request, CancellationToken cancellationToken)
        {
            var facet = await _unitOfWork.FacetRepository.GetByIdAsync(request.id);

            if (facet is null)
            {
                throw new ArgumentNullException(nameof(facet), "Facet not found");
            }

            await _unitOfWork.FacetRepository.DeleteByIdAsync(request.id);

            _unitOfWork.FacetRepository.Update(facet);

            await _unitOfWork.SaveAsync();

        }
    }
}
