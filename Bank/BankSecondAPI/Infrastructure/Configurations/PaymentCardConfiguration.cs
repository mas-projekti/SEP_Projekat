using BankApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Infrastructure.Configurations
{
    public class PaymentCardConfiguration : IEntityTypeConfiguration<PaymentCard>
    {
        public void Configure(EntityTypeBuilder<PaymentCard> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.ExipiringDate).IsRequired();
            builder.Property(x => x.CardNumber).IsRequired();
            builder.Property(x => x.SecurityCode).IsRequired();

            builder.HasOne(x => x.BankClient)
                .WithMany(x => x.PaymentCards)
                .HasForeignKey(x => x.BankClientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
