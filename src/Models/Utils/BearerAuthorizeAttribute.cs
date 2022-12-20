using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Models.Utils
{
    public class BearerAuthorizeAttribute : AuthorizeAttribute
    {
        public BearerAuthorizeAttribute()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
