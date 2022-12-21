using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Users;
using Models.Utils;
using Models.Utils.I18N;
using Models.ValueObjects;
using Newtonsoft.Json.Linq;
using Services.Authenticate.Requests;
using Services.Users;
using Services.Utils;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Authenticate
{
    public class JwtTokenService : IJwtTokenService
    {
        private const string ConfigKey = "Authentication:Secret";
        
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;

        private readonly string _secret;
        private readonly string _issuer = new("CT");
        private readonly string _audience = new("users");

        private readonly IJwtCacheStorage _jwtCache;

        public JwtTokenService(DatabaseContext context, IConfiguration configuration, IJwtCacheStorage jwtCache)
        {
            _context = context;
            _configuration = configuration;
            _jwtCache = jwtCache;
            _secret = configuration[ConfigKey];
        }

        public async Task<Jwt> AuthenticateAsync(UserCredentialRequests userCredential)
        {
            userCredential.ThrowIfNull(nameof(userCredential));
            
            User user = await UserOrFailAsync(userCredential.IIN);
            if (!user.HashedPassword().Same(userCredential.Password))
            {
                throw new InputValidationException(DataAnnotationErrorMessages.IncorrectIINOrPassword);
            }

            var key = _configuration.GetSection("JwtConfig:Key").Value;
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, user.Login),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var newJwt = new JwtToken(_secret, new PortalUserClaims(user).Identity(), _issuer, _audience);
            await _jwtCache.SaveJwtAsync(user.Id, newJwt);
            
            return newJwt;
        }

        private async Task<User> UserOrFailAsync(string email)
        {
            return await _context.UserOrNullAsync(email)
                   ?? throw new InputValidationException(DataAnnotationErrorMessages.IncorrectIINOrPassword);
        }
    }
}
