using Microsoft.EntityFrameworkCore;
using PSP.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Infrastructure
{
    public class PaymentServiceProviderDbContext : DbContext
    {
        public PaymentServiceProviderDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PspClient> PspClients { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentServiceProviderDbContext).Assembly);

        }
    }

}
