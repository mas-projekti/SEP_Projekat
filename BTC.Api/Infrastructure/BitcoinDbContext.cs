using BTC.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTC.Api.Infrastructure
{
    public class BitcoinDbContext : DbContext
    {
        public BitcoinDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BitcoinTransaction> BitcoinTransactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BitcoinDbContext).Assembly);

        }
    }
}
