using AutoMapper;
using DomainLayer.Common.Enums;
using DomainLayer.Entities;
using DomainLayer.Entities.Facets;
using DomainLayer.Entities.Products;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Commands.FacetCommands;
using ServiceLayer.Features.Commands.ProductCommands;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.CommandHandlers.FacetHandlers
{
    public class CreateFacetCommandHandler : IRequestHandler<CreateFacetCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateFacetCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(CreateFacetCommand request, CancellationToken cancellationToken)
        {
            var model = new FacetModel
            {
                Name = request.Name,
                DisplayType = request.DisplayType,
                IsCustom = request.IsCustom,
                FacetValues = new List<FacetValueModel>()
            };            

            if (request.FacetValues != null)
            {
                model.FacetValues = request.FacetValues.Select(v => new FacetValueModel
                {
                    Value = v.Value,              
                }).ToList();
            }

            var facet = _mapper.Map<Facet>(model);

            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.CategoryId);
            if(category == null)
            {
                throw new Exception("Category does not exist");
            }
            if(facet.Categories == null) {    
                facet.Categories = new List<Category>();
            }
            facet.Categories.Add(category);

            await _unitOfWork.FacetRepository.AddAsync(facet);
            await _unitOfWork.SaveAsync();

            return facet.Id;
        }
    }
}
