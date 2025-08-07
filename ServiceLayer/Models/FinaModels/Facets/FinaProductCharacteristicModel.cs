using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServiceLayer.Models.FinaModels.Facets
{
    public class FinaProductCharacteristicModel
    {
        [JsonPropertyName("product_id")]
        public int ProductId { get; set; }

        [JsonPropertyName("characteristic_id")]
        public int CharacteristicId { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }  // Default language

        [JsonPropertyName("value2")]
        public string? Value2 { get; set; }  // English

        [JsonPropertyName("value3")]
        public string? Value3 { get; set; }  // Russian/Ukrainian
    }
}
