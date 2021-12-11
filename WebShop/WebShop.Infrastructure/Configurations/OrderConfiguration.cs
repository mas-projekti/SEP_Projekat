using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShop.Models.DomainModels;
using WebShop.Models.Enums;

namespace WebShop.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.Timestamp)
                .IsRequired()
                .HasDefaultValue(DateTime.Now);     

            builder.Property(u => u.OrderStatus)
              .HasConversion<String>()
              .HasDefaultValue(OrderStatus.Pending);

            builder.HasOne(i => i.Customer)
               .WithMany(p => p.Orders)
               .HasForeignKey(i => i.CustomerId); 
        }
    }
}

