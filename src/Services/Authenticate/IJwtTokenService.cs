using Services.Authenticate.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Authenticate
{
    public interface IJwtTokenService
    {
        Task<string> AuthenticateAsync(UserCredentialRequests userCredential);
    }
}
