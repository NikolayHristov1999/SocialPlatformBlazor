using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialPlatformBlazor.Infrastructure
{
    public static class ClaimsPrincipalExtension
    {
        public static string? GetId(this ClaimsPrincipal user)
            => user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }
}
