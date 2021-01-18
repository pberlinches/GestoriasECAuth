using System.Threading.Tasks;
using ECAuth.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ECAuth.Services
{
    public class ECSignInManager : SignInManager<ApplicationUser>
    {
        public ECSignInManager(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<ApplicationUser>> logger, IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
        {
        }

        public override async Task<bool> CanSignInAsync(ApplicationUser user)
        {
            return await base.CanSignInAsync(user) && user.Enabled;
        }
    }
}