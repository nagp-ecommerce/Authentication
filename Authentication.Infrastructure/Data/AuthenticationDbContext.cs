using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Authentication.Domain;
using Authentication.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Data
{
    public class AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options)
        : DbContext(options)
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
