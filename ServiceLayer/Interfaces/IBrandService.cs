using ServiceLayer.Features.Commands.Brand;
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

        Task<Guid> AddAsync(CreateBrandCommand model);

        Task UpdateAsync(BrandModel model);

        Task DeleteAsync(Guid modelId);
    }
}
