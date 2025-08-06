using ServiceLayer.Models.FinaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces.FinaInterfaces
{
    public interface IProductGroupService
    {
        Task<IEnumerable<FinaProductGroupModel>> GetProductGroupsAsync();
        Task<IEnumerable<FinaProductGroupModel>> GetWebProductGroupsAsync();
        Task<FinaProductGroupModel> GetProductGroupByIdAsync(int id);
        Task<IEnumerable<FinaProductGroupModel>> GetChildGroupsAsync(int parentId);
    }
}
