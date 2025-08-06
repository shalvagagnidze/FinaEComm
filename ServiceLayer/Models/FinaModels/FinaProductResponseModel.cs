using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServiceLayer.Models.FinaModels
{
    public class FinaProductResponseModel
    {
        [JsonPropertyName("products")]
        public List<FinaProductModel>? Products { get; set; }

        [JsonPropertyName("ex")]
        public string? Ex { get; set; }
    }
}
