using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServiceLayer.Models.FinaModels
{
    public class FinaProductModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("group_id")]
        public int GroupId { get; set; }

        [JsonPropertyName("web_group_id")]
        public int? WebGroupId { get; set; }

        [JsonPropertyName("unit_id")]
        public int UnitId { get; set; }

        [JsonPropertyName("code")]
        public string? Code { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("name_eng")]
        public string? NameEng { get; set; }

        [JsonPropertyName("name_rus")]
        public string? NameRus { get; set; }

        [JsonPropertyName("comment")]
        public string? Comment { get; set; }

        [JsonPropertyName("partnumber")]
        public string? PartNumber { get; set; }

        [JsonPropertyName("weight")]
        public double Weight { get; set; }

        [JsonPropertyName("volume")]
        public double Volume { get; set; }

        [JsonPropertyName("vat")]
        public byte Vat { get; set; } 

        [JsonPropertyName("order_id")]
        public int OrderId { get; set; }

        [JsonPropertyName("min_quantity")]
        public string? MinQuantity { get; set; }

        [JsonPropertyName("add_fields")]
        public List<FinaAdditionalFieldModel>? AdditionalFields { get; set; }
    }
}
