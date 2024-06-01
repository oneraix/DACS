using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DACS.Models;
using Microsoft.AspNetCore.Authorization;

namespace DACS.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(username);
                var roles = await _userManager.GetRolesAsync(user);

                if (roles.Contains(Role.Role_Admin))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
                if (roles.Contains(Role.Role_QL))
                {
                    return RedirectToAction("Index", "Home", new { area = "QL" });
                }
                if (roles.Contains(Role.Role_KTV))
                {
                    return RedirectToAction("Index", "Home", new { area = "KTV" });
                }
                if (roles.Contains(Role.Role_GV))
                {
                    return RedirectToAction("Index", "Home", new { area = "GV" });
                }

                // Default redirect if no specific role found
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
