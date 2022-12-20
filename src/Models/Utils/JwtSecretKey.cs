using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Models.Utils
{
    public class JwtSecretKey : SymmetricSecurityKey
    {
        public JwtSecretKey(string key)
            : base(Encoding.UTF8.GetBytes(key))
        {
        }
    }
}