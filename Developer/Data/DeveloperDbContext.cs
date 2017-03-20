using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Developer.Models;
using AiursoftBase.Models;
using Microsoft.AspNetCore.Builder;

namespace Developer.Data
{
    public class DeveloperDbContext : IdentityDbContext<DeveloperUser>
    {
        public DeveloperDbContext(DbContextOptions<DeveloperDbContext> options)
            : base(options)
        {
        }

        public DbSet<App> Apps { get; set; }
        public DbSet<AppPermission> AppPermissions { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public async Task Seed(IApplicationBuilder app)
        {
            if (await Permissions.CountAsync() == 0)
            {
                this.Permissions.Add(new Permission
                {
                    PermissionName = "View your basic user info like your id and nickname"
                });
                this.Permissions.Add(new Permission
                {
                    PermissionName = "Change your user profile like your nickname"
                });
                this.Permissions.Add(new Permission
                {
                    PermissionName = "Change your password"
                });
            }
            await this.SaveChangesAsync();
        }
    }
}
