using Core.Idenrity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Commerceee.Extentions
{
    public static class UserManagerExtension
    {
        public static async Task<AppUser> findUserByClaimsPrinciplesWithAddress(this UserManager<AppUser> usermanager, 
            ClaimsPrincipal user)
        {
            var Email = user.FindFirstValue(ClaimTypes.Email);
            return await usermanager.Users.Include(x => x.Address).SingleOrDefaultAsync(x => x.Email == Email);

        }
        public static async Task<AppUser> findByEmailFromClaimsPrincipal(this UserManager<AppUser> usermanager,
            ClaimsPrincipal user)
        {
            return await usermanager.Users.SingleOrDefaultAsync(x => x.Email == user.FindFirstValue(ClaimTypes.Email));
        }
    }
}
