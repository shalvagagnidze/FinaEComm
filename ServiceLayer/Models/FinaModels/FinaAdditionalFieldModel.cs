using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServiceLayer.Models.FinaModels
{
    public class FinaAdditionalFieldModel
    {
        [JsonPropertyName("field")]
        public string? Field { get; set; }

        [JsonPropertyName("value")]
        public string? Value { get; set; }
    }
}
