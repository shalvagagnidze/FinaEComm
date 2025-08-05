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

public class DeleteFAQCommandHandler : IRequestHandler<DeleteFAQCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteFAQCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task Handle(DeleteFAQCommand request, CancellationToken cancellationToken)
    {
        var faq = await _unitOfWork.FAQRepository.GetByIdAsync(request.faq.Id);

        if (faq == null)
        {
            throw new ArgumentNullException(nameof(faq), "FAQ not found");
        }

        _unitOfWork.FAQRepository.Delete(faq);

        await _unitOfWork.SaveAsync();
    }
}
