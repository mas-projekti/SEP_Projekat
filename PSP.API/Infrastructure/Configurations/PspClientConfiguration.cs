using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PSP.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSP.API.Infrastructure.Configurations
{
    public class PspClientConfiguration : IEntityTypeConfiguration<PspClient>
    {
        public void Configure(EntityTypeBuilder<PspClient> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.ClientID).IsRequired();
            builder.Property(p => p.SettingsUpdatedCallback).IsRequired();
            builder.Property(p => p.TransactionOutcomeCallback).IsRequired();
            builder.Property(p => p.PaypalActive).HasDefaultValue(true);
            builder.Property(p => p.BitcoinActive).HasDefaultValue(false);
            builder.Property(p => p.BankActive).HasDefaultValue(false);
            builder.Property(p => p.ValidatingSecret).HasDefaultValue("");

        }
    }
}
