using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServiceLayer.Models.FinaModels.Facets
{
    public class FinaCharacteristicResponseModel
    {
        [JsonPropertyName("characteristics")]
        public List<FinaCharacteristicModel>? Characteristics { get; set; }

        [JsonPropertyName("ex")]
        public string? Ex { get; set; }
    }
}
