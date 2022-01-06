﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebShop.Infrastructure;

namespace WebShop.Infrastructure.Migrations
{
    [DbContext(typeof(WebShopDbContext))]
    [Migration("20211204224351_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebShop.Models.DomainModels.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Pending");

                    b.Property<DateTime?>("Timestamp")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2021, 12, 4, 23, 43, 51, 407, DateTimeKind.Local).AddTicks(4733));

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WebShop.Models.DomainModels.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasMaxLength(30)
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasMaxLength(30)
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("WebShop.Models.DomainModels.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("CategoryType")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Unknown");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ImageURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Manufacturer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OrderItemId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("Warranty")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderItemId")
                        .IsUnique()
                        .HasFilter("[OrderItemId] IS NOT NULL");

                    b.HasIndex("UserId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WebShop.Models.DomainModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BirthDay")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("MerchantId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Customer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebShop.Models.DomainModels.Order", b =>
                {
                    b.HasOne("WebShop.Models.DomainModels.User", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("WebShop.Models.DomainModels.OrderItem", b =>
                {
                    b.HasOne("WebShop.Models.DomainModels.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("WebShop.Models.DomainModels.Product", b =>
                {
                    b.HasOne("WebShop.Models.DomainModels.OrderItem", "OrderItem")
                        .WithOne("Product")
                        .HasForeignKey("WebShop.Models.DomainModels.Product", "OrderItemId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WebShop.Models.DomainModels.User", "User")
                        .WithMany("Products")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderItem");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebShop.Models.DomainModels.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("WebShop.Models.DomainModels.OrderItem", b =>
                {
                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebShop.Models.DomainModels.User", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}