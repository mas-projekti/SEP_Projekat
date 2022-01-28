using BankApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Infrastructure.Configurations
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.MoneyAmount).HasDefaultValue(0);
            builder.Property(x => x.ReservedMoneyAmount).HasDefaultValue(0);

            builder.HasOne(x => x.BankClient)
                .WithOne(x => x.BankAccount)
                .HasForeignKey<BankAccount>(x => x.BankClientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
