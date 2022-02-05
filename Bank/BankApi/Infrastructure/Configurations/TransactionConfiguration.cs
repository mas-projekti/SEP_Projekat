using BankApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Infrastructure.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasIndex(x => x.PaymentId).IsUnique();

            builder.HasOne(x => x.BankClient)
                .WithMany(x => x.Transactions)
                .HasForeignKey(x => x.BankClientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
