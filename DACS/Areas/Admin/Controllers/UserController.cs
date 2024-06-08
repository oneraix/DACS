using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using DACS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DACS.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(string searchTerm, string roleName)
        {
            IEnumerable<ApplicationUser> users;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = await _userManager.Users
                    .Where(u => u.Email.Contains(searchTerm) || u.FullName.Contains(searchTerm))
                    .ToListAsync();
            }
            else if (!string.IsNullOrEmpty(roleName))
            {
                users = await _userManager.GetUsersInRoleAsync(roleName);
            }
            else
            {
                users = await _userManager.Users.ToListAsync();
            }

            ViewBag.Roles = _roleManager.Roles;
            ViewBag.SelectedRole = roleName;

            return View(users);
        }

        public async Task<IActionResult> Create()
        {
            var roles = await _roleManager.Roles.Where(r => r.Name != "Admin").ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Name", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ApplicationUser user, string role, string password)
        {
            if (ModelState.IsValid)
            {
                user.UserName = user.Email; // Đảm bảo UserName là Email
                var result = await _userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(role))
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }

                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            var roles = await _roleManager.Roles.Where(r => r.Name != "Admin").ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Name", "Name", role);
            return View(user);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _roleManager.Roles.Where(r => r.Name != "Admin").ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Name", "Name");

            var userRoles = await _userManager.GetRolesAsync(user);
            ViewBag.UserRoles = userRoles;

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ApplicationUser user, string role)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByIdAsync(user.Id);
                if (existingUser == null)
                {
                    return NotFound();
                }

                existingUser.Email = user.Email;
                existingUser.FullName = user.FullName;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.MaNhanVien = user.MaNhanVien;

                var result = await _userManager.UpdateAsync(existingUser);

                if (result.Succeeded)
                {
                    var currentRoles = await _userManager.GetRolesAsync(existingUser);
                    await _userManager.RemoveFromRolesAsync(existingUser, currentRoles);
                    if (!string.IsNullOrEmpty(role))
                    {
                        await _userManager.AddToRoleAsync(existingUser, role);
                    }

                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            var roles = await _roleManager.Roles.Where(r => r.Name != "Admin").ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Name", "Name", role);

            return View(user);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(user);
        }
    }
}
