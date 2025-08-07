using ServiceLayer.Models.FinaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces.FinaInterfaces
{
    public interface IFinaIntegrationService
    {
        Task<SyncResult> SyncAllProductsAsync();
        Task<SyncResult> SyncProductByIdAsync(int finaProductId);
        Task<SyncResult> SyncProductGroupsAsync();
        Task<SyncResult> SyncCharacteristicsAsync();
    }
}
