using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models.DomainModels;

namespace WebShop.Infrastructure.Configurations
{
    public class PaymentOptionConfiguration : IEntityTypeConfiguration<PaymentOption>
    {
        public void Configure(EntityTypeBuilder<PaymentOption> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .ValueGeneratedOnAdd();       
        }
    }
}
