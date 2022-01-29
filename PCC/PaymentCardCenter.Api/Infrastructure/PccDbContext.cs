using Microsoft.EntityFrameworkCore;
using PaymentCardCenter.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCardCenter.Api.Infrastructure
{
    public class PccDbContext : DbContext
    {
        public DbSet<PanRegistryEntry> Entries { get; set; }
        public PccDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PccDbContext).Assembly);
        }
    }
}
