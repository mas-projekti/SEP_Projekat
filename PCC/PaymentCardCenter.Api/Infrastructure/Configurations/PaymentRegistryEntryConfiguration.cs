using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentCardCenter.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentCardCenter.Api.Infrastructure.Configurations
{
    public class PanRegistryEntryConfiguration : IEntityTypeConfiguration<PanRegistryEntry>
    {
        public void Configure(EntityTypeBuilder<PanRegistryEntry> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasIndex(x => x.PaymentCardPrefix).IsUnique();
        }
    }
}
