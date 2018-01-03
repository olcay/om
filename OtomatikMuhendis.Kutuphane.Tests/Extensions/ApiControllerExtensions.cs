using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace OtomatikMuhendis.Kutuphane.Tests.Extensions
{
    public static class ApiControllerExtensions
    {
        public static void MockCurrentUser(this Controller controller, string userId, string username)
        {
            var identity = new GenericIdentity(username);
            identity.AddClaim(new Claim(ClaimTypes.Name, username));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));

            var principal = new GenericPrincipal(identity, null);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = principal }
            };
        }
    }
}
