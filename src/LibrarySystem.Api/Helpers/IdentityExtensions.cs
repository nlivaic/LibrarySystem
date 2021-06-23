using System;
using System.Linq;
using System.Security.Claims;

namespace LibrarySystem.Api.Helpers
{
    public static class IdentityExtensions
    {
        public static Guid? UserId(this ClaimsPrincipal user)
        {
            var subClaim = user.Claims.SingleOrDefault(c => c.Type == "sub");
            return subClaim == null
                ? (Guid?)null
                : new Guid(subClaim.Value);
        }
    }
}
