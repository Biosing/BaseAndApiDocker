using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Services.Utils;
using Models.Utils;

namespace Models.Users
{
    public record CurrentUser
    {
        public long Id { get; }
        public string Login { get; }

        public CurrentUser(ClaimsPrincipal principal)
        {
            principal.ThrowIfNull(nameof(principal));

            if (!principal.HasClaims())
            {
                throw new ArgumentException("Principal does not have any claim");
            }

            Id = long.Parse(principal.GetClaimValue(ClaimTypes.NameIdentifier));
            Login = principal.GetClaimValue(ClaimTypes.Name);
        }


    }
}
