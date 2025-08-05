using AutoMapper;
using DomainLayer.Interfaces;
using InfrastructureLayer.Data;
using MediatR;
using ServiceLayer.Features.Queries.FAQQueries;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.QueryHandlers.FAQQueryHandlers;

public class GetFAQByIdQueryHandler : IRequestHandler<GetFAQByIdQuery, FAQModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetFAQByIdQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<FAQModel> Handle(GetFAQByIdQuery request, CancellationToken cancellationToken)
    {
        var model = await _unitOfWork.FAQRepository.GetByIdAsync(request.id);

        var faq = _mapper.Map<FAQModel>(model);

        return faq;
    }
}

