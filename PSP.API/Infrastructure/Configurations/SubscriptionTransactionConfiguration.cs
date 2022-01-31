using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Infrastructure.Configurations
{
    public class SubscriptionTransactionConfiguration : IEntityTypeConfiguration<SubscriptionTransaction>
    {
        public void Configure(EntityTypeBuilder<SubscriptionTransaction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Transaction)
                   .WithOne(x => x.SubscriptionTransaction)
                   .HasForeignKey<SubscriptionTransaction>(x => x.TransactionId);
        }
    }
}
