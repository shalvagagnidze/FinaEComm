using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Commands.FAQCommands;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.CommandHandlers.FAQCommandHandlers;

public class CreateFAQCommandHandler : IRequestHandler<CreateFAQCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateFAQCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Guid> Handle(CreateFAQCommand request, CancellationToken cancellationToken)
    {
        var existingFAQs = await _unitOfWork.FAQRepository.GetAllAsync();
        var maxOrderNum = existingFAQs.Any() ? existingFAQs.Max(f => f.OrderNum) : 0;

        int? targetOrderNum;

        if (request.OrderNum == null || request.OrderNum <= 0)
        {
            targetOrderNum = maxOrderNum + 1;
        }
        else
        {
            targetOrderNum = request.OrderNum;

            if (targetOrderNum > maxOrderNum + 1)
            {
                targetOrderNum = maxOrderNum + 1;
            }
            else if (targetOrderNum <= maxOrderNum)
            {
                var faqsToUpdate = existingFAQs
                    .Where(f => f.OrderNum >= targetOrderNum)
                    .OrderBy(f => f.OrderNum)
                    .ToList();

                foreach (var faqToUpdate in faqsToUpdate)
                {
                    faqToUpdate.OrderNum++;
                    _unitOfWork.FAQRepository.Update(faqToUpdate);
                }
            }
        }
        var model = new FAQModel
        {
            Question = request.Question,
            Answer = request.Answer,
            IsActive = request.IsActive,
            IsFeatured = request.IsFeatured,
            OrderNum = targetOrderNum,
        };

        var faq = _mapper.Map<FAQ>(model);
        await _unitOfWork.FAQRepository.AddAsync(faq);
        await _unitOfWork.SaveAsync();

        return faq.Id;
    }
}
