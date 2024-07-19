using AutoMapper;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.CommandHandlers.Brand
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommandHandler, BrandModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateBrandCommandHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public Task<BrandModel> Handle(UpdateBrandCommandHandler request, CancellationToken cancellationToken)
        {
            //Implement
        }
    }
}
