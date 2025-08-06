using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServiceLayer.Models.FinaModels
{
    public class FinaProductPriceModel
    {
        [JsonPropertyName("product_id")]
        public int ProductId { get; set; }

        [JsonPropertyName("price_id")]
        public int PriceId { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("discount_price")]
        public decimal DiscountPrice { get; set; }

        [JsonPropertyName("currency")]
        public string? Currency { get; set; }

        [JsonPropertyName("discount_start")]
        public DateTime? DiscountStart { get; set; }

        [JsonPropertyName("discount_end")]
        public DateTime? DiscountEnd { get; set; }
    }
}
