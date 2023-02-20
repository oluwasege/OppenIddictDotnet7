using IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OpenIddictDotnet7.Core.AspnetCore.Identity
{
    public class UserPrincipal : ClaimsPrincipal
    {
        public UserPrincipal(ClaimsPrincipal principal) : base(principal)
        {

        }

        private string? GetClaimValue(string key)
        {
            if (Identity is not ClaimsIdentity identity)
                return null;

            var claim = identity.Claims.FirstOrDefault(c => c.Type == key);
            return claim?.Value;
        }

        public string? UserName => FindFirst(JwtClaimTypes.Id) == null ? string.Empty : GetClaimValue(JwtClaimTypes.Id);

        public string? Email => FindFirst(JwtClaimTypes.Email) == null ? string.Empty : GetClaimValue(JwtClaimTypes.Email);

        public int UserId => FindFirst(JwtClaimTypes.Subject) == null ? default : Convert.ToInt32(GetClaimValue(JwtClaimTypes.Subject));

        public string FirstName
        {
            get
            {
                var usernameClaim = FindFirst(JwtClaimTypes.GivenName);

                return usernameClaim == null ? string.Empty : usernameClaim.Value;
            }
        }

        public string LastName
        {
            get
            {
                var usernameClaim = FindFirst(JwtClaimTypes.FamilyName);

                return usernameClaim == null ? string.Empty : usernameClaim.Value;
            }
        }

        //public string TenantId
        //{
        //    get
        //    {
        //        var tenantIdClaim = FindFirst(CoreConstants.ClaimsKey.TenantId);
        //        if (tenantIdClaim is null)
        //            return string.Empty;
        //        return tenantIdClaim.Value;
        //    }
        //}
    }
}
