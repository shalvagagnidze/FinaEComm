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
    public class DeleteCategoryByIdCommandHandler : IRequestHandler<DeleteCategoryByIdCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteCategoryByIdCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.id);

            if(category is null)
            {
                throw new ArgumentNullException(nameof(category), "Category not found");
            }

            await _unitOfWork.CategoryRepository.DeleteByIdAsync(request.id);

            category.DeleteTime = DateTime.UtcNow;

            _unitOfWork.CategoryRepository.Update(category);

            await _unitOfWork.SaveAsync();
        }
    }
}
