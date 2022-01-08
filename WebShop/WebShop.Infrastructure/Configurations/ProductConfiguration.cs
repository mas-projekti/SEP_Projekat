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

            builder.HasData(
                new Product { 
                    Id = 1, 
                    Amount = 50, 
                    CategoryType = CategoryType.Keyboard, 
                    Model = "Alloy Origins 60", 
                    Manufacturer = "HyperX", 
                    Description = "Make room for more plays", 
                    ImageURL = "https://media.kingston.com/hyperx/category/hx-family-keyboard-alloy-origins-60-md.jpg", 
                    UserId = 1, 
                    Price = 100, 
                    Warranty = 2,     
                },
                new Product
                {
                    Id = 2,
                    Amount = 30,
                    CategoryType = CategoryType.Mouse,
                    Model = "XL-747H Blue Spider",
                    Manufacturer = "X7",
                    Description = "Prestrafe in cs 1.6 with this",
                    ImageURL = "http://x7.a4tech.com/alanUpload/colorImg/img/201804/2310381700519639.jpg",
                    UserId = 1,
                    Price = 100,
                    Warranty = 2,
                },
                new Product
                {
                    Id = 3,
                    Amount = 40,
                    CategoryType = CategoryType.CPU,
                    Model = "Threadripper™ PRO 3995WX",
                    Manufacturer = "AMD Ryzen™",
                    Description = "Skupo jako",
                    ImageURL = "https://m.media-amazon.com/images/I/71xmfgJcw5L._AC_SL1500_.jpg",
                    UserId = 1,
                    Price = 500,
                    Warranty = 3,
                }
            );

        }
    }
}
