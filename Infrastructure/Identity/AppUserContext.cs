using Core.Idenrity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class AppUserContext:IdentityDbContext<AppUser>
    {
        public AppUserContext(DbContextOptions<AppUserContext> options):base(options) 
        {

        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Address> Adresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
