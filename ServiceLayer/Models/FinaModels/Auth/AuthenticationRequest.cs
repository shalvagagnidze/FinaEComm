using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ServiceLayer.Models.FinaModels.Auth
{
    public class AuthenticationRequest
    {
        [JsonPropertyName("login")]
        public string? Login { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}
