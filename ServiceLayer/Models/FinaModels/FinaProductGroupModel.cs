using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServiceLayer.Models.FinaModels
{
    public class FinaProductGroupModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("parent_id")]
        public int? ParentId { get; set; }

        [JsonPropertyName("order_id")]
        public int OrderId { get; set; }

        [JsonPropertyName("path")]
        public string? Path { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        public int GetDepthLevel()
        {
            if (string.IsNullOrEmpty(Path))
                return 0;

            return Path.Split('#').Length - 1;
        }

        public List<int> GetParentIds()
        {
            if (string.IsNullOrEmpty(Path))
                return new List<int>();

            return Path.Split('#')
                .Where(p => !string.IsNullOrEmpty(p) && p != "0")
                .Select(p => int.TryParse(p, out var id) ? id : 0)
                .Where(id => id > 0 && id != Id)
                .ToList();
        }

        public bool IsChildOf(int parentId)
        {
            return ParentId == parentId || GetParentIds().Contains(parentId);
        }
    }
}
