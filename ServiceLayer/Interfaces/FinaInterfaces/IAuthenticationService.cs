using ServiceLayer.Models.FinaModels.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces.FinaInterfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(string login, string password);
    }
}
