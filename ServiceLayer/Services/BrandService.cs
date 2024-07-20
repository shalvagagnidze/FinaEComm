using AutoMapper;
using DomainLayer.Entities;
using DomainLayer.Interfaces;
using MediatR;
using ServiceLayer.Features.Commands.BrandCommands;
using ServiceLayer.Features.Queries;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class BrandService : BaseService,IBrandService
    {
        private readonly ISender _sender;
        public BrandService(IUnitOfWork unitOfWork, IMapper mapper, ISender sender) : base(unitOfWork, mapper)
        {
            _sender = sender;
        }

        public async Task<IEnumerable<BrandModel>> GetAllAsync()
        {
            var brands = await _sender.Send(new GetAllBrandsQuery());

            return brands;
        }

        public async Task<BrandModel> GetByIdAsync(Guid id)
        {
            var brand = await _sender.Send(new GetBrandByIdQuery(id));

            return brand;
        }

        public async Task<Guid> AddAsync(CreateBrandCommand command)
        {
           var brandId = await _sender.Send(command);

           return brandId;
        }

        public async Task<BrandModel> UpdateAsync(BrandModel model)
        {
            var brand = await _sender.Send(new UpdateBrandCommand(model));

            return brand;
        }

        public async Task Delete(BrandModel model)
        {
            await _sender.Send(new DeleteBrandCommand(model));
        }
        public async Task DeleteAsync(Guid id)
        {
            await _sender.Send(new DeleteBrandByIdCommand(id));
        }
    }
}
