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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
           
            builder.HasKey(i => i.Id );

            builder.Property(i => i.Id)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.Description)
                 .HasMaxLength(500);

            builder.Property(i => i.Manufacturer)
                 .IsRequired();

            builder.Property(i => i.Model)
                .IsRequired();

            builder.Property(i => i.Price)
                .HasPrecision(18, 6)
                .IsRequired();

            builder.Property(u => u.CategoryType)
                .IsRequired()
                .HasConversion<String>()
                .HasDefaultValue(CategoryType.Unknown);

            builder.Property(i => i.Warranty)
                .IsRequired();

            builder.Property(i => i.ImageURL)
                .IsRequired();

            builder.HasOne(i => i.User)
                .WithMany(p => p.Products)
                .HasForeignKey(i => i.UserId);

            builder.HasOne(i => i.OrderItem)
              .WithOne(p => p.Product)
              .HasForeignKey<Product>(i => i.OrderItemId)
              .IsRequired(false)
               .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
