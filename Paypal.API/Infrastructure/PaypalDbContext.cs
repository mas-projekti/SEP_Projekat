using Microsoft.EntityFrameworkCore;
using Paypal.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.Infrastructure
{
    public class PaypalDbContext : DbContext
    {
        public PaypalDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<PaypalTransaction> PaypalTransaction { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaypalDbContext).Assembly);

        }
    }
}
