using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paypal.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paypal.API.Infrastructure.Configurations
{
    public class PaypalTransactionConfiguration : IEntityTypeConfiguration<PaypalTransaction>
    {
        public void Configure(EntityTypeBuilder<PaypalTransaction> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
