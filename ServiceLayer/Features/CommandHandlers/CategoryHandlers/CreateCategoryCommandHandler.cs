using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Commands.CategoryCommands;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.CommandHandlers.CategoryHandlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Guid> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var model = new CategoryModel
            {
                Name = request.Name,
                Description = request.Description,
            };

            var category = _mapper.Map<Category>(model);

            category.CreatedDate = DateTime.UtcNow;

            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveAsync();

            return category.Id;
        }
    }
}
