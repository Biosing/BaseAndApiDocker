using Models;
using Services.Authenticate.Requests;

namespace Services.Authenticate
{
    public interface IJwtTokenService
    {
        Task<Jwt> AuthenticateAsync(UserCredentialRequests userCredential);
    }
}
