using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServiceLayer.Models.FinaModels.Facets
{
    public class FinaProductCharacteristicResponseModel
    {
        [JsonPropertyName("characteristic_values")]
        public List<FinaProductCharacteristicModel>? CharacteristicValues { get; set; }

        [JsonPropertyName("ex")]
        public string? Ex { get; set; }
    }
}
