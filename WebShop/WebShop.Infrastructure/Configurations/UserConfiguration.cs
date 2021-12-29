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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.Username)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(i => i.Email)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(i => i.Password)
                 .IsRequired();

            builder.Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(i => i.Lastname)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(u => u.UserType)
                .HasConversion<String>()
                .HasDefaultValue(UserType.Customer);

            builder.Property(i => i.BirthDay)
                .IsRequired(false);

            builder.Property(i => i.ImageURL)
                .IsRequired(false);

            builder.HasData(
                new User { 
                    Id = 1, 
                    Name = "John", 
                    Lastname = "Doe", 
                    BirthDay = new DateTime(1990, 12, 12), 
                    Email = "johndoe@email.com", 
                    ImageURL = "https://upload.wikimedia.org/wikipedia/commons/f/fc/Zodiac-Killer.jpg", 
                    Username = "johndoe@email.com", 
                    Password = "secret", 
                    UserType = UserType.Salesman, 
                    MerchantId = Guid.NewGuid().ToString()
                },
                new User
                {
                    Id = 2,
                    Name = "Mira",
                    Lastname = "Markovic",
                    BirthDay = new DateTime(1990, 12, 12),
                    Email = "miramarkovic@email.com",
                    ImageURL = "https://upload.wikimedia.org/wikipedia/en/thumb/0/07/Mirjana_Markovi%C4%87.webp/302px-Mirjana_Markovi%C4%87.webp.png",
                    Username = "miramarkovic@email.com",
                    Password = "secret",
                    UserType = UserType.Customer,
                    MerchantId = Guid.NewGuid().ToString()
                }
            );
  
        }
            
    } 
}
