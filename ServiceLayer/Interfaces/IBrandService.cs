using MediatR;
using ServiceLayer.Features.Commands.BrandCommands;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IBrandService 
    {
        Task<IEnumerable<BrandModel>> GetAllAsync();

        Task<BrandModel> GetByIdAsync(Guid id);

        Task<Guid> AddAsync(CreateBrandCommand command);

        Task<Unit> UpdateAsync(BrandModel model);

        Task DeleteAsync(Guid Id);

        Task Delete(BrandModel model);
    }
}
