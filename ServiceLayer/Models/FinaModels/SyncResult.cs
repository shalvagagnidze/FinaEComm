using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models.FinaModels
{
    public class SyncResult
    {
        public bool Success { get; set; }
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
        public List<string> ProcessedItems { get; set; } = new();
        public List<string> Errors { get; set; } = new();
    }
}
