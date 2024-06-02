using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System;
using DACS.Models;

namespace DACS.Services
{
    public class CustomSignInManager : SignInManager<ApplicationUser>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CustomSignInManager(UserManager<ApplicationUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<ApplicationUser>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<ApplicationUser> confirmation)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        public override async Task SignInAsync(ApplicationUser user, bool isPersistent, string authenticationMethod = null)
        {
            await base.SignInAsync(user, isPersistent, authenticationMethod);

            var roles = await UserManager.GetRolesAsync(user);
            var context = _contextAccessor.HttpContext;

            if (roles.Contains("Admin"))
            {
                context.Response.Redirect("/Admin/Home/Index");
            }
            else if (roles.Contains("GV"))
            {
                context.Response.Redirect("/GV/Home/Index");
            }
            else if (roles.Contains("KTV"))
            {
                context.Response.Redirect("/KTV/Home/Index");
            }
            else if (roles.Contains("QL"))
            {
                context.Response.Redirect("/QL/Home/Index");
            }
            else
            {
                context.Response.Redirect("/");
            }
        }
    }
}
