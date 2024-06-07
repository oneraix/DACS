using DACS.Models;
using DACS.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DACS.Areas.QL.Controllers
{
    [Area("QL")]
    [Authorize(Roles = Role.Role_QL)]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(IUserRepository userRepository, RoleManager<IdentityRole> roleManager)
        {
            _userRepository = userRepository;
            _roleManager = roleManager;
        }

        // GET: Users
        public async Task<IActionResult> Index(string searchTerm, string roleName)
        {
            IEnumerable<ApplicationUser> users;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = await _userRepository.SearchUsersAsync(searchTerm);
            }
            else if (!string.IsNullOrEmpty(roleName))
            {
                users = await _userRepository.GetUsersByRoleAsync(roleName);
            }
            else
            {
                users = await _userRepository.GetAllUsersAsync();
            }

            ViewBag.Roles = _roleManager.Roles;
            ViewBag.SelectedRole = roleName;

            return View(users);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUser user, string password)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.CreateUserAsync(user, password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userRepository.UpdateUserAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var result = await _userRepository.DeleteUserAsync(id);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
