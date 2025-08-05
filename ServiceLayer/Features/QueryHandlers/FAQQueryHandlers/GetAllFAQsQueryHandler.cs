using AutoMapper;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Queries.FAQQueries;
using ServiceLayer.Models;

namespace ServiceLayer.Features.QueryHandlers.FAQQueryHandlers;

public class GetAllFAQsQueryHandler : IRequestHandler<GetAllFAQsQuery, IEnumerable<FAQModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GetAllFAQsQueryHandler(IUnitOfWork unitOfWork,IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<IEnumerable<FAQModel>> Handle(GetAllFAQsQuery request, CancellationToken cancellationToken)
    {
        var models = await _unitOfWork.FAQRepository.GetAllAsync();

        if (models == null)
        {
            return Enumerable.Empty<FAQModel>();
        }
        var faqs = _mapper.Map<IEnumerable<FAQModel>>(models);  

        return faqs;
    }
}

