using Database;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models.Users;
using Services.Authenticate.Requests;
using Services.Users;
using Services.Utils;
using Services.Utils.I18N;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services.Authenticate
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;

        public JwtTokenService(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> AuthenticateAsync(UserCredentialRequests userCredential)
        {
            userCredential.ThrowIfNull(nameof(userCredential));

            User user = await UserOrFailAsync(userCredential.IIN);

            if (!user.HashedPassword().Same(userCredential.Password))
            {
                throw new InputValidationException(DataAnnotationErrorMessages.IncorrectEmailOrPassword);
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
            return tokenHandler.WriteToken(token);
        }

        private async Task<User> UserOrFailAsync(string email)
        {
            return await _context.UserOrNullAsync(email)
                   ?? throw new InputValidationException(DataAnnotationErrorMessages.IncorrectEmailOrPassword);
        }
    }
}
