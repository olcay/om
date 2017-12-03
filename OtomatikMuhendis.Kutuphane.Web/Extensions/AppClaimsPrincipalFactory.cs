using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OtomatikMuhendis.Kutuphane.Web.Core.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OtomatikMuhendis.Kutuphane.Web.Extensions
{
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public AppClaimsPrincipalFactory(
            UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager
            , IOptions<IdentityOptions> options) 
            : base(userManager, roleManager, options)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);

            if (!string.IsNullOrWhiteSpace(user.Name))
            {
                ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
                    new Claim(ClaimTypes.GivenName, user.Name)
                });
            }

            return principal;
        }
    }
}
