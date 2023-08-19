using Core.Idenrity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AppIdentityUserSeed
    {
        public static async Task AddUserAdminAsync(UserManager<AppUser> userManager) { 
            if (!userManager.Users.Any()) {
                var user = new AppUser()
                {
                    DisplayName="Admin",
                    UserName="Admin",
                    Email="Admin@Admin.com",
                    Address=new Address
                    {
                        FirstName="admin",
                        LastName="admin",
                        Street="Omar ibn Khattab",
                        ZipCode="12354",
                        City="cairo",



                    }


                };
                await userManager.CreateAsync(user, "p@assword");
            }
        }
    }
}
