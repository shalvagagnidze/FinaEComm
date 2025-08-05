using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Commands.FAQCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.CommandHandlers.FAQCommandHandlers;

public class UpdateFAQCommandHandler : IRequestHandler<UpdateFAQCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    public UpdateFAQCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Unit> Handle(UpdateFAQCommand request, CancellationToken cancellationToken)
    {
        var existingFaq = await _unitOfWork.FAQRepository.GetByIdAsync(request.model.Id);

        if (existingFaq == null)
        {
            throw new ArgumentNullException(nameof(existingFaq), "FAQ not found");
        }

        var currentOrderNum = existingFaq.OrderNum;
        var newOrderNum = request.model.OrderNum;

        if (currentOrderNum != newOrderNum)
        {
            var otherFAQs = (await _unitOfWork.FAQRepository.GetAllAsync())
                .Where(f => f.Id != request.model.Id)
                .ToList();

            var maxOrderNum = otherFAQs.Any() ? otherFAQs.Max(f => f.OrderNum) : 0;

            int? targetOrderNum;

            if (newOrderNum == null || newOrderNum <= 0)
            {
                targetOrderNum = maxOrderNum + 1;
            }
            else
            {
                targetOrderNum = newOrderNum.Value;

                if (targetOrderNum > maxOrderNum + 1)
                {
                    targetOrderNum = maxOrderNum + 1;
                }
            }

            if (currentOrderNum < targetOrderNum)
            {
                var faqsToShiftUp = otherFAQs
                    .Where(f => f.OrderNum > currentOrderNum && f.OrderNum <= targetOrderNum)
                    .ToList();

                foreach (var faq in faqsToShiftUp)
                {
                    faq.OrderNum--;
                    _unitOfWork.FAQRepository.Update(faq);
                }
            }
            else if (currentOrderNum > targetOrderNum)
            {
                var faqsToShiftDown = otherFAQs
                    .Where(f => f.OrderNum >= targetOrderNum && f.OrderNum < currentOrderNum)
                    .ToList();

                foreach (var faq in faqsToShiftDown)
                {
                    faq.OrderNum++;
                    _unitOfWork.FAQRepository.Update(faq);
                }
            }
            existingFaq.OrderNum = targetOrderNum;
        }

        existingFaq.Question = request.model.Question;
        existingFaq.Answer = request.model.Answer;
        existingFaq.IsActive = request.model.IsActive;
        existingFaq.IsFeatured = request.model.IsFeatured;

        _unitOfWork.FAQRepository.Update(existingFaq);
        await _unitOfWork.SaveAsync();

        return Unit.Value;
    }
}
