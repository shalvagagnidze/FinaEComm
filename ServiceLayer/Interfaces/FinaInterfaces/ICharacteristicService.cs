using ServiceLayer.Models.FinaModels.Facets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces.FinaInterfaces;

public interface ICharacteristicService
{
    Task<IEnumerable<FinaCharacteristicModel>> GetCharacteristicsAsync();
    Task<IEnumerable<FinaProductCharacteristicModel>> GetCharacteristicValuesAsync();
}
