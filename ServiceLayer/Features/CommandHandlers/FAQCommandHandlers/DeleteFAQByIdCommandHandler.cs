using DomainLayer.Entities;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Commands.FAQCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.CommandHandlers.FAQCommandHandlers;

public class DeleteFAQByIdCommandHandler : IRequestHandler<DeleteFAQByIdCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteFAQByIdCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(DeleteFAQByIdCommand request, CancellationToken cancellationToken)
    {
        var faq = await _unitOfWork.FAQRepository.GetByIdAsync(request.Id);

        if (faq == null)
        {
            throw new ArgumentNullException(nameof(faq), "FAQ not found");
        }

        await _unitOfWork.FAQRepository.DeleteByIdAsync(request.Id);

        await _unitOfWork.SaveAsync();
    }
}
