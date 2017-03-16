using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Account.Models;

namespace Account.Data
{
    public class AccountDbContext : IdentityDbContext<AccountUser>
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options)
            : base(options)
        {

        }
        public DbSet<Organization> Organizations { get; set; }
    }
}
