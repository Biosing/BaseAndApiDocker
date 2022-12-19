using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Authenticate;
using Services.Authenticate.Requests;

namespace BaseAndApiDocker.Controllers
{
    [AllowAnonymous]
    [Route("api/auth")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenManager;
        public AuthenticateController(IJwtTokenService jwtTokenManager)
        {
            _jwtTokenManager = jwtTokenManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateAsync([FromBody] UserCredentialRequests userCredantial)
        {
            var token = await _jwtTokenManager.AuthenticateAsync(userCredantial);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
