using BankApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankApi.Infrastructure.Configurations
{
    public class BankClientConfiguration : IEntityTypeConfiguration<BankClient>
    {
        public void Configure(EntityTypeBuilder<BankClient> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasIndex(x => x.Username).IsUnique();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.ClientType).HasConversion<string>();
        }
    }
}
