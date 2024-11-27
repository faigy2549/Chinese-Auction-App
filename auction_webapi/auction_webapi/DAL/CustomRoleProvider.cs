using auction_webapi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using System.Reflection.Emit;

namespace auction_webapi.DAL
{
    public class CustomRoleProvider:IRoleProvider
    {
        private readonly AuctionContext _context;

        public CustomRoleProvider(AuctionContext context)
        {
            _context = context;
        }

        public Task<bool> RoleExistsAsync(EnumRoles roleName)
        {
            if (roleName == EnumRoles.admin || roleName == EnumRoles.user)
                return Task.FromResult(true);
            return Task.FromResult(false);
        }

        public async Task<IList<EnumRoles>> GetRolesAsync(User user)
        {
            var roles = await _context.Users
                    .Where(ur => ur.UserId == user.UserId)
                    .Select(ur => ur.Role)
                    .ToListAsync();

            return roles;
        }

    }
}
