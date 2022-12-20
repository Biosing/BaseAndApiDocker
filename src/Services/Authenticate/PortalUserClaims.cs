using Models.Users;
using Services.Utils;
using System.Security.Claims;

namespace Services.Authenticate
{
    internal class PortalUserClaims
    {
        private User _user;

        public PortalUserClaims(User user)
        {
            user.ThrowIfNull(nameof(user));
            _user = user;
        }

        public ClaimsPrincipal Principal()
        {
            return new ClaimsPrincipal(Identity());
        }

        public ClaimsIdentity Identity()
        {
            return new ClaimsIdentity(
                claims: Claims(),
                authenticationType: "ApplicationCookie",
                nameType: ClaimsIdentity.DefaultNameClaimType,
                roleType: ClaimsIdentity.DefaultRoleClaimType);
        }

        private IEnumerable<Claim> Claims()
        {
            yield return new Claim(ClaimsIdentity.DefaultNameClaimType, _user.IIN);
            yield return new Claim(ClaimTypes.Name, _user.Login);
            yield return new Claim(ClaimTypes.NameIdentifier, _user.Id.ToString());
        }
    }
}