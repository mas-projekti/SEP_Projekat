using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Infrastructure.Configurations
{
    public class BankTransactionConfiguration : IEntityTypeConfiguration<BankTransaction>
    {
        public void Configure(EntityTypeBuilder<BankTransaction> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Transaction)
                   .WithOne(x => x.BankTransaction)
                   .HasForeignKey<BankTransaction>(x => x.TransactionId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
