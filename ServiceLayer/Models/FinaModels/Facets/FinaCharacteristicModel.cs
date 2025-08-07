using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServiceLayer.Models.FinaModels.Facets
{
    public class FinaCharacteristicModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("tag")]
        public string? Tag { get; set; }

        [JsonPropertyName("type")]
        public byte Type { get; set; }  // 0=text, 1=list

        [JsonPropertyName("name")]
        public string? Name { get; set; }  // Default name

        [JsonPropertyName("name2")]
        public string? Name2 { get; set; }  // English

        [JsonPropertyName("name3")]
        public string? Name3 { get; set; }  // Russian/Ukrainian
    }
}
