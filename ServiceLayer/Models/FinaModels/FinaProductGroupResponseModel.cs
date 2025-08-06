using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServiceLayer.Models.FinaModels
{
    public class FinaProductGroupResponseModel
    {
        [JsonPropertyName("groups")]
        public List<FinaProductGroupModel>? Groups { get; set; }

        [JsonPropertyName("ex")]
        public string? Ex { get; set; }
    }
}
