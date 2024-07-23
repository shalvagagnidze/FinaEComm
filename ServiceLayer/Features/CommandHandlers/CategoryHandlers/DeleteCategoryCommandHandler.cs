using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Commands.CategoryCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.CommandHandlers.CategoryHandlers
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.model.Id);

            if(category is null)
            {
                throw new ArgumentNullException(nameof(category), "Category not found");
            }

            _unitOfWork.CategoryRepository.Delete(category);

            category.DeleteTime = DateTime.UtcNow;

            _unitOfWork.CategoryRepository.Update(category);

            await _unitOfWork.SaveAsync();
        }
    }
}
