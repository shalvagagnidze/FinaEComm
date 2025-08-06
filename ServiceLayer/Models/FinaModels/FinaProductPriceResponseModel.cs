using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServiceLayer.Models.FinaModels
{
    public class FinaProductPriceResponseModel
    {
        [JsonPropertyName("prices")]
        public List<FinaProductPriceModel>? Prices { get; set; }

        [JsonPropertyName("ex")]
        public string? Ex { get; set; }
    }
}
