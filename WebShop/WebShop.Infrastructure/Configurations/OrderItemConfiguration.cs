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
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.Amount)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(i => i.Price)
                .IsRequired()
                .HasPrecision(18, 6)
                .HasMaxLength(30);

            builder.HasOne(i => i.Order)
              .WithMany(p => p.OrderItems)
              .HasForeignKey(i => i.OrderId);


        }
    }
}
