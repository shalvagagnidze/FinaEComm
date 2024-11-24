using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Models
{
    public sealed class S3Settings
    {
        public string Region { get; set; } = string.Empty;
        public string BucketName {  get; set; } = string.Empty;

    }
}
