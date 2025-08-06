using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.FinaServices
{
    public class TokenStorageService
    {
        private static string? _token;
        private static DateTime _tokenExpiration;

        public void SetToken(string token)
        {
            _token = token;
            _tokenExpiration = DateTime.UtcNow.AddHours(36);
        }

        public string GetToken()
        {
            if (string.IsNullOrEmpty(_token) || DateTime.UtcNow >= _tokenExpiration)
            {
                return "";
            }
            return _token;
        }

        public bool IsTokenValid()
        {
            return !string.IsNullOrEmpty(_token) && DateTime.UtcNow < _tokenExpiration;
        }
    }
}
