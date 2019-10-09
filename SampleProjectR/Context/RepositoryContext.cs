using Microsoft.EntityFrameworkCore;
using SampleProjectR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProjectR.Context
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options) { }
        public DbSet<Owners> Owners { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
