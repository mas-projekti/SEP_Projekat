using BTC.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BTC.Api.Infrastructure
{
    public class BitcoinTransactionConfiguration : IEntityTypeConfiguration<BitcoinTransaction>
    {
        public void Configure(EntityTypeBuilder<BitcoinTransaction> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
