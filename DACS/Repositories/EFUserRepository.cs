using DACS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DACS.Repositories
{
    public class EFUserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EFUserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> UpdateUserAsync(ApplicationUser user)
        {
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.DeleteAsync(user);
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersByRoleAsync(string roleName)
        {
            var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
            return usersInRole;
        }

        public async Task<IEnumerable<ApplicationUser>> SearchUsersAsync(string searchTerm)
        {
            return await _userManager.Users
                .Where(u => u.Email.Contains(searchTerm) ||
                            u.FullName.Contains(searchTerm) ||
                            u.PhoneNumber.Contains(searchTerm) ||
                            u.MaNhanVien.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
