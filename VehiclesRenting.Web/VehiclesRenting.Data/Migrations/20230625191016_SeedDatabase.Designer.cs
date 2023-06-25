﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VehiclesRenting.Data;

#nullable disable

namespace VehiclesRenting.Data.Migrations
{
    [DbContext(typeof(VehiclesRentingDbContext))]
    [Migration("20230625191016_SeedDatabase")]
    partial class SeedDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("VehiclesRenting.Data.Models.Agent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Agents");
                });

            modelBuilder.Entity("VehiclesRenting.Data.Models.Car", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AgentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CurrentAddress")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("PricePerDay")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid?>("RenterId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AgentId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RenterId");

                    b.ToTable("Cars");

                    b.HasData(
                        new
                        {
                            Id = new Guid("aef33f88-dd3a-45ba-aa2b-2572e5cbab9e"),
                            AgentId = new Guid("b18534f1-c32b-401f-b740-6035cb456174"),
                            Brand = "Hyundai",
                            CategoryId = 1,
                            Color = "Black",
                            CurrentAddress = "Silistra, Center",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/4/4b/Hyundai_Santa_Fe_%28TM%29_PHEV_FL_IMG_6648.jpg",
                            Model = "Santa fe",
                            PricePerDay = 40m,
                            RegistrationNumber = "CC1835AX",
                            RenterId = new Guid("3d2d9e6d-038b-4ff5-90b8-4eebc4c48426")
                        },
                        new
                        {
                            Id = new Guid("26d29059-c4e9-4758-9d93-cff07d85ff25"),
                            AgentId = new Guid("b18534f1-c32b-401f-b740-6035cb456174"),
                            Brand = "Mercedes",
                            CategoryId = 1,
                            Color = "White",
                            CurrentAddress = "Silistra, Center",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/55/Mercedes-Benz_W223_IMG_6663.jpg/1200px-Mercedes-Benz_W223_IMG_6663.jpg",
                            Model = "S-Class",
                            PricePerDay = 70m,
                            RegistrationNumber = "CB0832AP"
                        },
                        new
                        {
                            Id = new Guid("44196c29-7d1c-4dee-8d56-b39833062ada"),
                            AgentId = new Guid("b18534f1-c32b-401f-b740-6035cb456174"),
                            Brand = "Ford Mustang",
                            CategoryId = 1,
                            Color = "Gray",
                            CurrentAddress = "Silistra, East",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/f/f6/1967_Ford_Mustang_Shelby_GT-500_Eleanor.jpg",
                            Model = "GT500 Shelby",
                            PricePerDay = 200m,
                            RegistrationNumber = "CC0000CC"
                        });
                });

            modelBuilder.Entity("VehiclesRenting.Data.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Cars"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Motorcycle"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Scooter"
                        });
                });

            modelBuilder.Entity("VehiclesRenting.Data.Models.Motorcycle", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AgentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CurrentAddress")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("PricePerDay")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<Guid?>("RenterId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AgentId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RenterId");

                    b.ToTable("Motorcycles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3732d1b9-30f0-42e5-a2a1-c30d411f7cd1"),
                            AgentId = new Guid("b18534f1-c32b-401f-b740-6035cb456174"),
                            Brand = "Honda",
                            CategoryId = 2,
                            Color = "Red",
                            CurrentAddress = "Ruse, Center",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/06/Honda750NR.jpg/1200px-Honda750NR.jpg",
                            Model = "NR-L",
                            PricePerDay = 30m,
                            RegistrationNumber = "CC3552OB",
                            RenterId = new Guid("3d2d9e6d-038b-4ff5-90b8-4eebc4c48426")
                        },
                        new
                        {
                            Id = new Guid("89c4f559-5494-460f-81a3-9398d2cd10d3"),
                            AgentId = new Guid("b18534f1-c32b-401f-b740-6035cb456174"),
                            Brand = "Suzuki",
                            CategoryId = 2,
                            Color = "Blue",
                            CurrentAddress = "Silistra, Center",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/f/f6/GSXR1000K5.jpg/640px-GSXR1000K5.jpg",
                            Model = "GSX-R1000K5",
                            PricePerDay = 75m,
                            RegistrationNumber = "CC1550AB"
                        },
                        new
                        {
                            Id = new Guid("96994854-db0d-403f-af85-da66bcde294b"),
                            AgentId = new Guid("b18534f1-c32b-401f-b740-6035cb456174"),
                            Brand = "Harley-Davidson",
                            CategoryId = 2,
                            Color = "Gray",
                            CurrentAddress = "Silistra, West",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/1/13/Harley_5-06.jpg",
                            Model = "VRSC",
                            PricePerDay = 110m,
                            RegistrationNumber = "CC1200AK"
                        });
                });

            modelBuilder.Entity("VehiclesRenting.Data.Models.Scooter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AgentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CurrentAddress")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.Property<decimal>("PricePerDay")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid?>("RenterId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AgentId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("RenterId");

                    b.ToTable("Scooters");

                    b.HasData(
                        new
                        {
                            Id = new Guid("c4763f6b-ab41-480c-8805-93ddc03cef42"),
                            AgentId = new Guid("b18534f1-c32b-401f-b740-6035cb456174"),
                            Brand = "Xiomi",
                            CategoryId = 3,
                            CurrentAddress = "Silistra, Center",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a2/Xiaomi_M365.jpg/1200px-Xiaomi_M365.jpg",
                            PricePerDay = 12m,
                            RenterId = new Guid("3d2d9e6d-038b-4ff5-90b8-4eebc4c48426")
                        },
                        new
                        {
                            Id = new Guid("63fe1ba5-b370-4550-b11d-fea51b7b8ff9"),
                            AgentId = new Guid("b18534f1-c32b-401f-b740-6035cb456174"),
                            Brand = "E-scooter",
                            CategoryId = 3,
                            CurrentAddress = "Silistra, North",
                            ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/29/Elektrische-tretroller.jpg/800px-Elektrische-tretroller.jpg",
                            PricePerDay = 7m
                        },
                        new
                        {
                            Id = new Guid("0c589107-6bed-4e05-b43c-5465140bfaa2"),
                            AgentId = new Guid("b18534f1-c32b-401f-b740-6035cb456174"),
                            Brand = "Inmotion",
                            CategoryId = 3,
                            CurrentAddress = "Silistra, West",
                            ImageUrl = "https://cdn.shopify.com/s/files/1/0021/7389/4702/products/LeMotion-Web-1.jpg?v=1636106454",
                            PricePerDay = 9m
                        });
                });

            modelBuilder.Entity("VehiclesRenting.Data.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrentAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("VehiclesRenting.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("VehiclesRenting.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VehiclesRenting.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("VehiclesRenting.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("VehiclesRenting.Data.Models.Agent", b =>
                {
                    b.HasOne("VehiclesRenting.Data.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("VehiclesRenting.Data.Models.Car", b =>
                {
                    b.HasOne("VehiclesRenting.Data.Models.Agent", "Agent")
                        .WithMany("ManagedCars")
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("VehiclesRenting.Data.Models.Category", "Category")
                        .WithMany("Cars")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("VehiclesRenting.Data.Models.User", "Renter")
                        .WithMany("RentedCars")
                        .HasForeignKey("RenterId");

                    b.Navigation("Agent");

                    b.Navigation("Category");

                    b.Navigation("Renter");
                });

            modelBuilder.Entity("VehiclesRenting.Data.Models.Motorcycle", b =>
                {
                    b.HasOne("VehiclesRenting.Data.Models.Agent", "Agent")
                        .WithMany("ManagedMotorcycles")
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("VehiclesRenting.Data.Models.Category", "Category")
                        .WithMany("Motorcycles")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("VehiclesRenting.Data.Models.User", "Renter")
                        .WithMany("RentedMotorcycles")
                        .HasForeignKey("RenterId");

                    b.Navigation("Agent");

                    b.Navigation("Category");

                    b.Navigation("Renter");
                });

            modelBuilder.Entity("VehiclesRenting.Data.Models.Scooter", b =>
                {
                    b.HasOne("VehiclesRenting.Data.Models.Agent", "Agent")
                        .WithMany("ManagedScooters")
                        .HasForeignKey("AgentId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("VehiclesRenting.Data.Models.Category", "Category")
                        .WithMany("Scooters")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("VehiclesRenting.Data.Models.User", "Renter")
                        .WithMany("RentedScooters")
                        .HasForeignKey("RenterId");

                    b.Navigation("Agent");

                    b.Navigation("Category");

                    b.Navigation("Renter");
                });

            modelBuilder.Entity("VehiclesRenting.Data.Models.Agent", b =>
                {
                    b.Navigation("ManagedCars");

                    b.Navigation("ManagedMotorcycles");

                    b.Navigation("ManagedScooters");
                });

            modelBuilder.Entity("VehiclesRenting.Data.Models.Category", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("Motorcycles");

                    b.Navigation("Scooters");
                });

            modelBuilder.Entity("VehiclesRenting.Data.Models.User", b =>
                {
                    b.Navigation("RentedCars");

                    b.Navigation("RentedMotorcycles");

                    b.Navigation("RentedScooters");
                });
#pragma warning restore 612, 618
        }
    }
}
