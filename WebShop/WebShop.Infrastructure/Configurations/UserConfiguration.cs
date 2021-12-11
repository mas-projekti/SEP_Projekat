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


  
        }
            
    } 
}
