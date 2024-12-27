﻿// <auto-generated />
using System;
using TemplateProject.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace TemplateProject.Migrations
{
    [DbContext(typeof(TemplateProjectDbContext))]
    [Migration("20240908123034_UpdateModel")]
    partial class UpdateModel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.8");

            modelBuilder.Entity("TemplateProject.Repositories.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Brands");
                });

            modelBuilder.Entity("TemplateProject.Repositories.Models.Dealership", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("LocaleId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("LocaleId");

                    b.ToTable("Dealerships");
                });

            modelBuilder.Entity("TemplateProject.Repositories.Models.Locale", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("RegionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Locales");
                });

            modelBuilder.Entity("TemplateProject.Repositories.Models.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Logo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Theme")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("TemplateProject.Repositories.Models.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BrandId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("TemplateProject.Repositories.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TemplateProject.Repositories.Models.UserAccess", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AccessLevel")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BrandId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("DealershipId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ManufacturerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("DealershipId");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("UserId");

                    b.ToTable("UserAccess");
                });

            modelBuilder.Entity("TemplateProject.Repositories.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BrandId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("VIN")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("TemplateProject.Repositories.Models.Brand", b =>
                {
                    b.HasOne("TemplateProject.Repositories.Models.Manufacturer", "Manufacturer")
                        .WithMany("Brands")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("TemplateProject.Repositories.Models.Dealership", b =>
                {
                    b.HasOne("TemplateProject.Repositories.Models.Locale", "Locale")
                        .WithMany("Dealerships")
                        .HasForeignKey("LocaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Locale");
                });

            modelBuilder.Entity("TemplateProject.Repositories.Models.Locale", b =>
                {
                    b.HasOne("TemplateProject.Repositories.Models.Region", "Region")
                        .WithMany("Locales")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Region");
                });

            modelBuilder.Entity("TemplateProject.Repositories.Models.Region", b =>
                {
                    b.HasOne("TemplateProject.Repositories.Models.Brand", "Brand")
                        .WithMany("Regions")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");
                });

            modelBuilder.Entity("TemplateProject.Repositories.Models.UserAccess", b =>
                {
                    b.HasOne("TemplateProject.Repositories.Models.Brand", "Brand")
                        .WithMany("UserAccess")
                        .HasForeignKey("BrandId");

                    b.HasOne("TemplateProject.Repositories.Models.Dealership", "Dealership")
                        .WithMany("UserAccess")
                        .HasForeignKey("DealershipId");

                    b.HasOne("TemplateProject.Repositories.Models.Manufacturer", "Manufacturer")
                        .WithMany("UserAccess")
                        .HasForeignKey("ManufacturerId");

                    b.HasOne("TemplateProject.Repositories.Models.User", "User")
                        .WithMany("UserAccess")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Dealership");

                    b.Navigation("Manufacturer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TemplateProject.Repositories.Models.Vehicle", b =>
                {
                    b.HasOne("TemplateProject.Repositories.Models.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TemplateProject.Repositories.Models.Manufacturer", "Manufacturer")
                        .WithMany("Vehicles")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brand");

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("TemplateProject.Repositories.Models.Brand", b =>
                {
                    b.Navigation("Regions");

                    b.Navigation("UserAccess");
                });

            modelBuilder.Entity("TemplateProject.Repositories.Models.Dealership", b =>
                {
                    b.Navigation("UserAccess");
                });

            modelBuilder.Entity("TemplateProject.Repositories.Models.Locale", b =>
                {
                    b.Navigation("Dealerships");
                });

            modelBuilder.Entity("TemplateProject.Repositories.Models.Manufacturer", b =>
                {
                    b.Navigation("Brands");

                    b.Navigation("UserAccess");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("TemplateProject.Repositories.Models.Region", b =>
                {
                    b.Navigation("Locales");
                });

            modelBuilder.Entity("TemplateProject.Repositories.Models.User", b =>
                {
                    b.Navigation("UserAccess");
                });
#pragma warning restore 612, 618
        }
    }
}
